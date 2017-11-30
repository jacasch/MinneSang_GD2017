using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {
    public float jumpTakeOffSpeed = 7f;
    public float maxSpeed = 7;
    public float dashDuration = 0.5f;

    protected bool dashing = false;
    protected float dashTimer;
    protected TrailRenderer tr;
    protected float trailDelay;

    protected override void Initialize()
    {
        tr = GetComponent<TrailRenderer>();
        tr.enabled = false;
        trailDelay = tr.time;
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        trailDelay -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
            groundNormal = Vector2.up;
        }

        if (Input.GetButtonDown("Dash"))
        {
            dashing = true;
            tr.enabled = true;
            dashTimer = dashDuration;            
        }
        if (dashing) {
            dashTimer -= Time.deltaTime;
            if (dashTimer > 0)
            {
                //Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
                move.x += dashDirection.x * 5;
                velocity.y = 0f;
                //velocity.y = dashDirection.y * 5 * (-Physics2D.gravity.y / 3);
                trailDelay = tr.time;
            }
            else {
                dashing = false;
                move.x = 0;
                velocity.y = 0;
            }
            
        }
        if (trailDelay < 0)
        {
            tr.enabled = false;
        }

        targetVelocity = move * maxSpeed;
    }
}
