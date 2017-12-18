using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {
    public float jumpTakeOffSpeed = 7f;
    public float maxSpeed = 7;
    public float dashDuration = 0.5f;
    public float knockbackintensity = 4f;
    public int maxDashesInAir = 2;

    protected bool knockedBack = false;
    protected bool dashing = false;
    protected float dashTimer;
    protected TrailRenderer tr;
    protected float trailDelay;
    protected float dashAnimationDelay;
    [HideInInspector]
    public bool inNpcZone = false;
    protected bool canDash;
    protected PlayerGui pg;
    protected PlayerStats ps;
    protected PlayerStun playerStun;
    [HideInInspector]
    public int dashCount = 0;
    private float dashCooldown = 1f;
    public int dashesLeft = 2;
    private float dashCooldownTime;
    public SpriteRenderer sr;
    private Animator animator;
    public bool aiming;
    private PlayerSoundHandler psh;
    private float lastStepTime;
    private float stepInterval = 0.35f;
    Vector2 dashDirection = new Vector2(0, 0);
    private float lastXInput = 0;

    private bool canMove;
    private bool attacking;
    public bool dead = false;

    bool isLanded = false;

    protected override void Initialize()
    {
        tr = GetComponent<TrailRenderer>();
        pg = GetComponent<PlayerGui>();
        ps = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        playerStun = GameObject.FindGameObjectWithTag("StunToEnemy").GetComponent<PlayerStun>();
        tr.enabled = false;
        trailDelay = tr.time;
        dashAnimationDelay = 0.5f;
        inNpcZone = false;
        sr = GetComponent<SpriteRenderer>();
        psh = GetComponent<PlayerSoundHandler>();
    }

    protected override void ComputeVelocity()
    {
        lastXInput = Input.GetAxis("Horizontal") != 0 ? Input.GetAxis("Horizontal") : lastXInput;

        Vector2 move = Vector2.zero;
        trailDelay -= Time.deltaTime;
        dashAnimationDelay -= Time.deltaTime;

        if(!isLanded && grounded && !dead)
        {
            psh.Land();
            isLanded = true;
        }
        else if(!grounded)
        {
            isLanded = false;
        }

        #region Input

        attacking = animator.GetBool("Attacking");
        dead = animator.GetBool("Dead");
        bool poetryCasting = ps.poetryCasting > 0;
        bool stunCasting = playerStun.castTimer > 0;
        canMove = !poetryCasting && !stunCasting && (!aiming || !grounded) && !dead;
        //dashCount = grounded ? 0 : dashCount;
        if (dashesLeft >= 2)
        {
            dashCooldownTime = Time.time;
            dashesLeft = 2;
        }
        else if (dashesLeft < 0) {
            dashesLeft = 0;
        }

        //dash cooldown
        if (dashCooldownTime + dashCooldown  < Time.time) {
            dashesLeft++;
            dashCooldownTime = Time.time;
        }

        canDash = dashesLeft > 0;

        animator.SetBool("Grounded", grounded);
        animator.SetFloat("VelocityY", velocity.y);
        //animator.SetBool("Attacking", Input.GetButtonDown("Attack"));

        if (!knockedBack)
        {
            animator.SetBool("Walking", false);
            if (canMove)
            {                
                float input = Input.GetAxis("Horizontal");
                move.x = input;
                if (input != 0)
                {
                    PlayStep();
                    animator.SetBool("Walking", true);
                    if (!attacking)
                        sr.flipX = (Input.GetAxis("Horizontal") < 0);
                }
            }
        }
        else {
            if (grounded)
            {
                knockedBack = false;
            }
            else {
                move.x = knockbackintensity;
            }
        }

        if (Input.GetButtonDown("Jump") && grounded && !inNpcZone && canMove) {
            velocity.y = jumpTakeOffSpeed;
            groundNormal = Vector2.up;
            psh.Jump();
        }

        if (Input.GetButtonDown("Dash") && !inNpcZone && canMove && canDash && !dashing && pg.skillLevel >= 1) {
            psh.Dash();
            dashesLeft--;
            dashing = true;
            dashDirection = new Vector2(lastXInput, 0).normalized;
            print(dashDirection);
            tr.enabled = true;
            dashTimer = dashDuration;            
        }
        if (dashing && !knockedBack) {
            animator.SetBool("Dashing", true);
            dashTimer -= Time.deltaTime;
            if (dashTimer > 0)
            {
                //Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                //dashDirection = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
                move.x += dashDirection.x * 5;
                velocity.y = 0f;
                //velocity.y = dashDirection.y * 5 * (-Physics2D.gravity.y / 3);
                trailDelay = tr.time;
                dashAnimationDelay = 0.4f;
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
            //animator.SetBool("Dashing", false);
        }
        if (dashAnimationDelay < 0)
        {
            animator.SetBool("Dashing", false);
        }
        #endregion

        targetVelocity = move * maxSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NpcInteractionZone")
        {
            inNpcZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NpcInteractionZone")
        {
            inNpcZone = false;
        }
    }

    public void KnockBack(float intensity) {
        knockbackintensity = intensity;
        knockedBack = true;
        velocity.y = jumpTakeOffSpeed * Mathf.Abs(intensity)/2;
        grounded = false;
    }

    private void PlayStep() {
        if (lastStepTime <= Time.time - stepInterval / Mathf.Abs(Input.GetAxis("Horizontal"))
            && grounded
            && !dashing
            && canMove
            && !attacking
            && !GetComponent<AudioSource>().isPlaying) {
            //step
            lastStepTime = Time.time;
            psh.Step();
        }
    }
}
