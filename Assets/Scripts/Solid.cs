using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Solid : MonoBehaviour
{
    // TO-DO: add pixels per unit to game manager
    private const int pixelsPerUnit = 16;
    private float xRemainder = 0;
    private float yRemainder = 0;
    private BoxCollider2D collision;

    protected void Awake()
    {
        // get solid's collider
        this.collision = this.GetComponent<BoxCollider2D>();
    }

    public void Move(float xAmount, float yAmount)
    {
        // add movement amount to remainder
        xRemainder += xAmount;
        yRemainder += yAmount;

        // round remaining movement to int
        int xMove = Mathf.RoundToInt(xRemainder);
        int yMove = Mathf.RoundToInt(yRemainder);

        // only move if we have to move
        if (xMove != 0 || yMove != 0)
        {
            // get all actors riding this solid
            var ridingActors = ActorManager.actorManager.getAllRidingActors(this);

            // disable this solid's collision
            //this.collision.isTrigger = true;

            // only move x if we have to
            if (xMove != 0)
            {
                // remove the amount we move from the remainder
                xRemainder -= xMove;

                // move position
                this.transform.position += new Vector3(xMove * 1f / pixelsPerUnit, 0, 0);

                // pushing and riding
                if (xMove > 0)
                {
                    // move right
                    foreach (var item in ActorManager.actorManager.getAllActors())
                    {
                        var boundingBox = this.collision;
                        var actorBoundingBox = item.GetComponent<BoxCollider2D>();
                        Action squish = null;

                        // ensure that actor doesn't get squished when pushed off the left edge by a wall
                        if (item.transform.position.x > this.transform.position.x)
                            squish = item.squish;

                        // if actor is riding, move it along with the solid's movement
                        // otherwise move it along the edge of the platform and invoke squish when colliding with another object
                        if (checkCollision(actorBoundingBox).Count() > 0)
                            item.MoveX(pixelsPerUnit * ((this.transform.position.x + boundingBox.size.x / 2) - (item.transform.position.x - actorBoundingBox.size.x / 2)), squish);
                        else if (ridingActors.Contains(item))
                            item.MoveX(xMove);
                    }
                }
                else
                {
                    // move left
                    foreach (var item in ActorManager.actorManager.getAllActors())
                    {
                        var boundingBox = this.collision;
                        var actorBoundingBox = item.GetComponent<BoxCollider2D>();
                        Action squish = null;

                        if (item.transform.position.x < this.transform.position.x)
                            squish = item.squish;

                        if (checkCollision(actorBoundingBox).Count() > 0)
                            item.MoveX(pixelsPerUnit * ((this.transform.position.x - boundingBox.size.x / 2) - (item.transform.position.x + actorBoundingBox.size.x / 2)), squish);
                        else if (ridingActors.Contains(item))
                            item.MoveX(xMove);
                    }
                }
            }


            if (yMove != 0)
            {
                // remove the amount we move from the remainder
                yRemainder -= yMove;

                // move position
                this.transform.position += new Vector3(0, yMove * 1f / pixelsPerUnit, 0);

                // pushing and riding
                if (yMove > 0)
                {
                    // move upwards
                    foreach (var item in ActorManager.actorManager.getAllActors())
                    {
                        var boundingBox = this.collision;
                        var actorBoundingBox = item.GetComponent<BoxCollider2D>();

                        if (checkCollision(actorBoundingBox).Count() > 0)
                            item.MoveY(pixelsPerUnit * ((this.transform.position.y + boundingBox.size.y / 2) - (item.transform.position.y - actorBoundingBox.size.y / 2)), item.squish);
                        else if (ridingActors.Contains(item))
                            item.MoveY(yMove);
                    }
                }
                else
                {
                    // move downwards
                    foreach (var item in ActorManager.actorManager.getAllActors())
                    {
                        var boundingBox = this.collision;
                        var actorBoundingBox = item.GetComponent<BoxCollider2D>();

                        if (checkCollision(actorBoundingBox).Count() > 0)
                            item.MoveY(pixelsPerUnit * ((this.transform.position.y - boundingBox.size.y / 2) - (item.transform.position.y + actorBoundingBox.size.y / 2)), item.squish);
                        else if (ridingActors.Contains(item))
                            item.MoveY(yMove);
                    }
                }
            }
        }

        // re-enable collision
        //this.collision.isTrigger = false;
    }

    private IEnumerable<Collider2D> checkCollision(BoxCollider2D other)
    {
        return Physics2D.OverlapBoxAll(this.transform.position,
                                       this.collision.size - new Vector2(1f / pixelsPerUnit, 1f / pixelsPerUnit), 0)
                                       .Where(b => b == other);
    }
}