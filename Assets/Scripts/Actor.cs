using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

interface IEchoBreakable
{
    void Break();
}

interface IWhirlActivatable
{
    void Activate();
}

interface ILightActivatable
{
    void Activate();
}

interface IReversible
{
    IEnumerator Reverse();
}

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Actor : MonoBehaviour
{
    // TO-DO: add pixels per unit to game manager
    protected const int pixelsPerUnit = 16;
    protected float xRemainder = 0;
    protected float yRemainder = 0;
    protected BoxCollider2D colliderBox;

    protected int direction = 1;
    protected bool grounded = false;
    protected bool touchCeiling = false;
    protected SpriteRenderer rend;

    protected void Awake()
    {
        // get actor's box collider and sprite renderer
        colliderBox = this.GetComponent<BoxCollider2D>();
        rend = this.GetComponent<SpriteRenderer>();
    }

    protected void Start()
    {
        // register actor upon creation
        ActorManager.actorManager.register(this);
    }

    public void MoveX(float amount, Action collisionAction)
    {
        // add movement amount to remainder
        xRemainder += amount;

        // round remaining movement to int
        int move = Mathf.RoundToInt(xRemainder);

        // only move if we have to move
        if (move != 0)
        {
            // remove the amount we move from the remainder
            xRemainder -= move;

            // calculate movement direction
            int sign = Math.Sign(move);
            direction = sign;

            // repeat until we have moved the full amount
            while (move != 0)
            {
                if (checkCollision(new Vector3(sign * 1f / pixelsPerUnit, 0, 0)).Count() == 0)
                {
                    // if there's nothing to collid with after moving 1 pixel, move
                    this.transform.position += new Vector3(sign * 1f / pixelsPerUnit, 0, 0);
                    move -= sign;
                }
                else
                {
                    // otherwise if we'd collide, stop moving, check grounded/ceiling and invoke the collision event
                    checkSurrounding();
                    collisionAction?.Invoke();
                    break;
                }
            }
        }

        // check grounded/ceiling after moving
        checkSurrounding();
    }

    public void MoveY(float amount, Action collisionAction)
    {
        // add movement amount to remainder
        yRemainder += amount;

        // round remaining movement to int
        int move = Mathf.RoundToInt(yRemainder);

        // only move if we have to move
        if (move != 0)
        {
            // remove the amount we move from the remainder
            yRemainder -= move;

            // calculate movement direction
            int sign = Math.Sign(move);
            direction = sign;

            while (move != 0)
            {
                if (checkCollision(new Vector3(0, sign * 1f/pixelsPerUnit, 0)).Count() == 0)
                {
                    // if there's nothing to collid with after moving 1 pixel, move
                    this.transform.position += new Vector3(0, sign * 1f / pixelsPerUnit, 0);
                    move -= sign;
                }
                else
                {
                    // otherwise if we'd collide, stop moving, check grounded/ceiling and invoke the collision event
                    checkSurrounding();
                    collisionAction?.Invoke();
                    break;
                }
            }
        }
        // check grounded/ceiling after moving
        checkSurrounding();
    }

    public void MoveX(float amount)
    {
        MoveX(amount, null);
    }

    public void MoveY(float amount)
    {
        MoveY(amount, null);
    }

    public float GetDistance(Vector3 other)
    {
        var distanceVector = this.transform.position - other;
        return distanceVector.magnitude;
    }

    public bool isVisible()
    {
        if (rend != null)
            return rend.isVisible;
        else
            return false;
    }

    protected void checkSurrounding()
    {
        // initialise grounded to be false
        grounded = false;
        // if there are collisions right below us, set grounded to true
        if(checkCollision(new Vector3(0, -1f/pixelsPerUnit, 0)).Count() > 0)
            grounded = true;

        // initialise ceiling to be false
        touchCeiling = false;
        // if there are collisions right above us, set ceiling to true
        if (checkCollision(new Vector3(0, 1f / pixelsPerUnit, 0)).Count() > 0)
            touchCeiling = true;
    }

    private IEnumerable<RaycastHit2D> checkCollision(Vector3 offset)
    {
        // perform a boxcast with a given offset and ignore our own collider
        return Physics2D.BoxCastAll(this.transform.position + offset,
                                    colliderBox.bounds.size - new Vector3(1f / pixelsPerUnit, 1f / pixelsPerUnit, 0),
                                    0, Vector2.zero)
                                    .Where(b => b.collider != this.colliderBox);
    }

    public virtual bool isRiding(Solid ridable)
    {
        // if we are below the solid, we can't ride it
        if ((this.transform.position.y - this.colliderBox.size.y / 2) <= (ridable.transform.position.y - ridable.GetComponent<BoxCollider2D>().size.y / 2))
            return false;

        // check if we collide with the given solid
        return checkCollision(new Vector3(0, -2f/pixelsPerUnit, 0))
                .Where(b => b.transform == ridable.transform)
                .Count() > 0;
    }

    public virtual void squish() {
        // kill the actor
        Destroy(this.gameObject);
    }

    // unregister upon death
    private void OnDestroy()
    {
        ActorManager.actorManager.unregister(this);
    }
}
