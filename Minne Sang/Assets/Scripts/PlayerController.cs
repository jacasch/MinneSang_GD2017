using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {
    public float jumpTakeOffSpeed = 7f;
    public float maxSpeed = 7;
    public float dashDuration = 0.5f;

    protected bool dashing = false;
    protected float dashTimer;

	void Start () {
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        }

        if (Input.GetButtonDown("Dash"))
        {
            dashing = true;
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
            }
            else {
                dashing = false;
                move.x = 0;
                velocity.y = 0;
            }
        }

        targetVelocity = move * maxSpeed;
    }
}
