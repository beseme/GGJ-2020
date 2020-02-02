using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
//using UniRx;
//using UniRx.Triggers;
using UnityEngine.Rendering.PostProcessing;

public class PlayerController : Actor
{
    public float speed = 80f;
    public float jumpHeight = 48f;
    public float dashHeight = 48f;
    public float fuel = 3f;
    public bool hoverEnabled = false;
    public GameObject postpr;
    
    [Tooltip("Time the Player has to press the Jump Button after falling off an edge")]
    public float coyoteTime = 0.5f;
    [Tooltip("Time the Player has to press the Jump Button before landing")]
    public float jumpGraceTime = 0.5f;
    [Tooltip("Player Weight")]
    public float gravityMultiplier = 1f;

    // Particle Systems
    public GameObject landParticles;
    public GameObject jumpParticles;
    public GameObject hoverParticles;
    //public GameObject dashParticles;

    private enum PlayerState
    {
        moving,
        falling,
        jumping
    }

    private enum Nozzle
    {
        floating = 0,
        dashing = 1
    }

    private Vector2 movement;
    private Vector2 _stickAxis = Vector2.zero;
    private PlayerState currentState = PlayerState.moving;
    private Queue<Action> inputMessages = new Queue<Action>();
    private float coyoteTimer = 0;
    private float bufferTimer = 0;
    private float minJumpBuffer = 0;
    private float _stickVal = 0;
    private Animator anim;

    private float _triggerPressed = 0;
    //private SpriteRenderer sprite;
    private Nozzle currentNozzle = 0;
    private Input _controlls = null;

    /* ------------------------------------------------------------------ */
    /* Input Handling */
    /* ------------------------------------------------------------------ */

    private void OnEnable() => _controlls.InputPad.Enable();
    private void OnDisable() => _controlls.InputPad.Disable();
    new void Awake()
    {
        base.Awake();

        _controlls = new Input();

        _controlls.InputPad.Jump.performed += Button => initJump();
        _controlls.InputPad.Jump.canceled += Button => killJumpInit();
        _controlls.InputPad.Run.performed += Stick => _stickAxis = Stick.ReadValue<Vector2>();
        _controlls.InputPad.Run.canceled += Stick => _stickAxis = Vector2.zero;
        _controlls.InputPad.JetPack.performed += Trigger => _triggerPressed = Trigger.ReadValue<float>();
        _controlls.InputPad.JetPack.canceled += Trigger => _triggerPressed = 0;

        _controlls.Keyboard.Jump.performed += Key => initJump();
        _controlls.Keyboard.Jump.canceled += Key => killJumpInit();
        _controlls.Keyboard.RunLeft.performed += LKey => _stickVal = -LKey.ReadValue<float>();
        _controlls.Keyboard.RunLeft.canceled += LKey => _stickVal = 0;
        _controlls.Keyboard.RunRight.performed += RKey => _stickVal = RKey.ReadValue<float>();
        _controlls.Keyboard.RunRight.canceled += RKey => _stickVal = 0;
        _controlls.Keyboard.JetPack.performed += Trigger => _triggerPressed = Trigger.ReadValue<float>();
        _controlls.Keyboard.JetPack.canceled += Trigger => _triggerPressed = 0;
    }
    new void Start()
    {
        // register at actor manager
        base.Start();
        this.anim = this.GetComponent<Animator>();
        //this.sprite = this.GetComponent<SpriteRenderer>();

        // observe the jump key
        /*var jumpPressedStream = this.UpdateAsObservable()
                                    .Where(_ => Input.GetButtonDown("Jump"))
                                    .Subscribe(initJump);

        var jumpReleasedStream = this.UpdateAsObservable()
                                    .Where(_ => Input.GetButtonUp("Jump"))
                                    .Subscribe(killJumpInit);

        // observe the direction keys
        this.UpdateAsObservable()
            .Where(_ => Input.GetButton("Jump"))
            .Subscribe(hover);

        // observe the direction keys
        this.UpdateAsObservable()
            .Where(_ => Input.GetAxisRaw("Horizontal") != 0)
            .Subscribe(initSideMovement);

        this.UpdateAsObservable()
        .Where(_ => Input.GetButtonDown("Switch"))
        .Subscribe(switchNozzle);
        */
    }

   /* private void switchNozzle(Unit x)
    {
        currentNozzle = (Nozzle)(((int)currentNozzle + 1) % 2);
    }
*/
    private void hover()
    {
        if (currentNozzle == Nozzle.floating)
            this.inputMessages.Enqueue(doHover);
    }

    // initialise jump upon jump input
    private void initJump()
    {
        // if on ground or coyote jump timeframe enabled, send jump message to fixed update
        // otherwise set the buffer for the jump
        if (this.grounded || this.coyoteTimer > 0)
            this.inputMessages.Enqueue(startJump);
        else if (this.fuel <= 0)
            this.inputMessages.Enqueue(bufferJump);
        else if (this.currentNozzle == Nozzle.dashing)
            this.inputMessages.Enqueue(initDash);
    }

    private void killJumpInit()
    {
        //this.chroma.enabled.value = false;
        inputMessages.Enqueue(killJump);
    }

    // initialise the horizontal movement
    private void initSideMovement()
    {
        // send a message to fixed update with the horizontal values
        this.inputMessages.Enqueue(() => { setMovementX(_stickVal); });
    }


    private void Update() => _stickVal = (_stickAxis.x > 0 ? _stickAxis.magnitude : -_stickAxis.magnitude);

    /* ------------------------------------------------------------------ */
    /* Physics Update */
    /* ------------------------------------------------------------------ */
    void FixedUpdate()
    {
        if (this._stickVal > 0)
            this.transform.localScale = new Vector3(1, 1, 1);
        else if (this._stickVal < 0)
            this.transform.localScale = new Vector3(-1, 1, 1);

        /*if (fuel <= 0)
            sprite.color = new Color(255, 0, 0);
        else if (currentNozzle == 0)
            sprite.color = new Color(0, 255, 0);
        else
            sprite.color = new Color(255, 0, 255);*/

        initSideMovement();
        
        if(_triggerPressed == 1)
            hover();
        
        // Debug.Log(fuel);
        // execute input messeges
        this.listenToInput();

        // check if player is grounded or touching the ceiling
        this.checkSurrounding();

        // state machine logic
        switch (this.currentState)
        {
            case PlayerState.moving:
                this.updateMovement();
                break;
            case PlayerState.falling:
                this.updateFalling();
                break;
            case PlayerState.jumping:
                this.updateJumping();
                break;
            default:
                break;
        }

        // execute y movement
        this.MoveY(this.movement.y * Time.fixedDeltaTime);

        
    }

    /* State Machine States */
    /* Ground State */
    private void updateMovement()
    {
        if (Mathf.Abs(_stickVal) > 0.1f)
            this.anim.SetBool("walking", true);
        else
            this.anim.SetBool("walking", false);

        // ensure that the player is always grounded in this state
        if (!grounded)
        {
            this.anim.SetBool("walking", false);
            this.coyoteTimer = this.coyoteTime;
            this.currentState = PlayerState.falling;
        }

        // if a jump got buffered, execute it upon touching the ground
        if (this.bufferTimer > 0)
            this.startJump();

        // apply buffered jump kill
        if (this.minJumpBuffer > 0)
        {
            this.movement.y = this.minJumpBuffer;
            this.minJumpBuffer = 0;
        }
            
    }

    /* Falling State */
    private void updateFalling()
    {
        // apply gravity
        this.applyGravity();

        // calculate coyote time
        this.coyoteTimer -= Time.fixedDeltaTime;

        // reduce the buffered jump's timer
        this.bufferTimer -= Time.fixedDeltaTime;

        // go back to the grounded state upon hitting the ground
        land();
    }

    /* Jumping State */
    private void updateJumping()
    {
        this.anim.SetBool("jumping", true);
        // apply gravity
        this.applyGravity();

        // reduce the buffered jump's timer
        this.bufferTimer -= Time.fixedDeltaTime;

        // if the player touches the ceiling, make them fall down
        if (this.touchCeiling && this.movement.y > 0)
            this.movement.y = 0;

        if (this.movement.y <= 0)
            this.currentState = PlayerState.falling;

        // go back to the grounded state upon hitting the ground
        land();
    }

    /* Update Utility */
    private void land()
    {
        //this.chroma.enabled.value = false;
        if (this.grounded && this.movement.y <= 0)
        {
            this.anim.SetBool("jumping", false);
            this.currentState = PlayerState.moving;
            this.fuel = 3f;

            var bottomBounds = this.transform.position - new Vector3(0, this.colliderBox.size.y / 2, 0);
            Instantiate(this.landParticles, bottomBounds, Quaternion.identity);
        }
    }

    // Gravity calculation
    private void applyGravity()
    {
        if (!grounded)
            this.movement.y += Physics2D.gravity.y * Time.fixedDeltaTime * this.gravityMultiplier;
        else if (this.movement.y < 0)
            this.movement.y = 0;
    }

    /* Input Messages */
    // Jump Button Messages
    private void startJump()
    {
        // calculate amount to move to reach jump hight based on gravity
        this.movement.y = Mathf.Sqrt(2 * this.jumpHeight * Mathf.Abs(Physics2D.gravity.y * this.gravityMultiplier));

        // reset coyote timer
        this.coyoteTimer = 0;

        // initiate particles
        var bottomBounds = this.transform.position - new Vector3(0, this.colliderBox.size.y / 2, 0);
        Instantiate(this.jumpParticles, bottomBounds, Quaternion.identity);

        // enable jump state
        this.currentState = PlayerState.jumping;
    }

    private void bufferJump()
    {
        // set jump buffer timer
        this.bufferTimer = this.jumpGraceTime;
    }

    private void doHover()
    {
        if (this.currentState != PlayerState.falling || this.fuel <= 0)
            return;

        // reset coyote timer
        this.coyoteTimer = 0;
        this.bufferTimer = 0;

        var bottomBounds = this.transform.position - new Vector3(0, this.colliderBox.size.y / 2, 0);
        this.movement.y = 300;
        Instantiate(this.hoverParticles, bottomBounds, Quaternion.identity);
        this.fuel -= 5 * Time.fixedDeltaTime;
    }

    private void initDash()
    {
        if (this.currentState == PlayerState.moving)
            return;

        this.fuel = 0;

        // calculate amount to move to reach jump hight based on gravity
        var bottomBounds = this.transform.position - new Vector3(0, this.colliderBox.size.y / 2, 0);
        this.movement.y = Mathf.Sqrt(2 * this.dashHeight * Mathf.Abs(Physics2D.gravity.y * this.gravityMultiplier));
        //Instantiate(this.dashParticles, bottomBounds, Quaternion.identity);

        // enable jump state
        this.currentState = PlayerState.jumping;
    }

    private void killJump()
    {
        // calculate minimum movement
        var minMovement = Mathf.Sqrt(2 * 1f * Mathf.Abs(Physics2D.gravity.y * this.gravityMultiplier));

        // kill the jump by setting the movement to the minimum if it's higher
        if (this.movement.y > minMovement)
            this.movement.y = minMovement;

        // preserve jump kill if previous jump input got buffered
        if (this.bufferTimer > 0)
            this.minJumpBuffer = minMovement;
    }

    // Horizontal Axes Message
    private void setMovementX(float amount)
    {
        this.MoveX(amount * speed * Time.fixedDeltaTime);
    }

    // Execute Messages
    private void listenToInput()
    {
        if (this.inputMessages.Count() > 0)
        {
            // loop through all messages
            foreach (Action message in inputMessages.ToList())
            {
                // if message exists, invoke it and remove it from the queue
                message?.Invoke();
                this.inputMessages.Dequeue();
            }
        }
    }
}
