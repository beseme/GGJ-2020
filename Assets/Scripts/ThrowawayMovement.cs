using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowawayMovement : MonoBehaviour
{
    private Vector2 _stickAxis = Vector2.zero;
    private float _stickVal = 0;
    private RaycastHit2D _floorHit;
    private Rigidbody2D _rig = null;
    private Transform _self = null;
    private bool _selected = false;
    private bool _flying = false;
    private SpriteRenderer _colour = null;
    private float _nCD = 0;
    private float _maxFuel = 0;

    [SerializeField] private float _speed = 0;
    [SerializeField] private float _jumpForce = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _self = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_stickAxis.x > 0)
            _stickVal = _stickAxis.magnitude;
        else if (_stickAxis.x < 0)
            _stickVal = -_stickAxis.magnitude;
        else _stickVal = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
            RunL();
        if (Input.GetKey(KeyCode.RightArrow))
            RunR();
        if (Input.GetKeyDown(KeyCode.Space))
            Jumps();
    }

    private void FixedUpdate()
    {
        _rig.velocity = new Vector2(_stickVal * _speed, _rig.velocity.y);

        _floorHit = Physics2D.Raycast(_self.position - (Vector3)Vector2.up * 2, -Vector2.up, .8f);
    }

    

    void RunL() => _stickVal = -1;

    void RunR() => _stickVal = 1;

    void StandStill() => _stickVal = 0;

    void Jumps()
    {
        //if (_floorHit)
            _rig.AddForce(new Vector2(0, _jumpForce * 80));
    }    
}
