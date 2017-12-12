﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOoze : MonoBehaviour
{
    /*
    GegnerInfo:

    Enemy: Ooze
    Ability: None
    Movement: Hüpft in Richtung des Spielers.
    Attack: Bei Berührung gibt es Schaden.
    */

    //Eigenschaften des Gegners. (DMG ist untergeordnet im DMG Objekt festgelegt!)
    int hpMax = 2;  //MAX HP des Gegners
    int speed = 5;  //Geschwindigkeit des Gegners
    int jumpHeight = 7;  //Sprunghöhe
    float dist = 0.5f;  //Distanz ab welcher der Gegner stillsteht(X-Achse)
    float jumpCD = 0.2f;  //Zeit bis der Sprung nach der Landung erneut ausgeführt wird
    float timeStunned = 3;  //Zeit die der Gegner gestunnt ist wenn er gestunnt wird
    float deadTime = 0.75f;  //Zeit bis der Gegner nach dem Tot verschwindet
    float respawnTime = 30;  //Zeit bis der Gegner respawnt

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' beherscht.
    public bool isStealth = false;

    //ScriptVariables
    int hp;  //HP des Gegners

    bool active = false;
    bool grounded = false;
    bool stealth = false;
    bool rightUp = false;
    int jumpUp = 1;
    bool dead = false;

    int dir = 1;
    float halfSize = 0;  //Für CheckIfGrounded

    float jumpTimer = 0;
    float stunTimer = 0;
    float deadTimer = 0;
    float respawnTimer = 0;

    Vector3 orgPos;
    Vector3 deadPos = new Vector3(1000, 0, 0);
    Rigidbody2D rb;

    SpriteRenderer mySprite;
    public Material defaultMat;
    public Material chameleonMat;

    public EnemyDMG enemyDmg;
    public GameObject aura;
    EnemyDMG auraDmg;

    Animator animator;


    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start()
    {
        halfSize = GetComponent<BoxCollider2D>().size.y / 2;

        hp = hpMax;
        stealth = isStealth;
        orgPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        auraDmg = aura.GetComponent<EnemyDMG>();

        animator = GetComponent<Animator>();

        if (stealth)
        {
            mySprite.material = chameleonMat;
        }
        else
        {
            mySprite.material = defaultMat;
        }
    }

    void Update()
    {
        if (dead)
        {
            Die();
        }
        else if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        }
        else if (active)
        {
            Move();
            if (rightUp && rb.velocity.x == 0)
            {
                rb.velocity = new Vector3(1.25f * dir, rb.velocity.y, 0);
                rightUp = false;
            }

            if (rb.velocity.x == 0 && rb.velocity.y == 0 && !grounded && !rightUp && jumpUp == 1)
            {
                animator.SetBool("Grounded", true);
                rb.velocity = new Vector3(speed * dir, jumpHeight, 0);
            }
        }
        if(rb.velocity.y < 0)
        {
            animator.SetFloat("VelocityY", -1);
        }
        else
        {
            animator.SetFloat("VelocityY", 1);
        }
    }

    //FUNCTIONS------------------------------------------------------------------------------------------------------------
    //Movement des Gegners
    void Move()
    {
        if(grounded)
        {
            jumpTimer -= Time.deltaTime;
            if (dir < 0 && !stealth)
            {
                mySprite.flipX = false;
            }
            else
            {
                mySprite.flipX = true;
            }
            if(jumpTimer<=0)
            {
                jumpTimer = jumpCD;
                rb.velocity = new Vector3(speed * dir * jumpUp, jumpHeight, 0);
            }
        }
    }

    //Wenn der Gegner tot ist
    void Die()
    {
        enemyDmg.noDmg = true;
        auraDmg.noDmg = true;

        if (deadTimer > 0)
        {
            deadTimer -= Time.deltaTime;
            //DieAnimation
            //...
        }
        else
        {
            if (respawnTimer == respawnTime)
            {
                transform.position = deadPos;
            }
            if(respawnTimer < 0)
            {
                hp = hpMax;
                stealth = isStealth;
                if (stealth)
                {
                    mySprite.material = chameleonMat;
                }
                dead = false;
                animator.SetBool("dead", false);
                enemyDmg.noDmg = false;
                auraDmg.noDmg = false;
                transform.position = orgPos;
            }
            respawnTimer -= Time.deltaTime;
        }
    }

    //Wenn der Player den Gegner angreift
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stealth)
        {
            if (collision.gameObject.tag == "Paint")
            {
                stealth = false;
                mySprite.material = defaultMat;
                if (dir < 0)
                {
                    mySprite.flipX = true;
                }
                else
                {
                    mySprite.flipX = false;
                }
            }
        }
        else
        {
            if (collision.gameObject.tag == "DmgToEnemy")
            {
                hp -= 1;
                rb.velocity = new Vector3(5 * -dir, 5, 0);
                print("ENEMY HP: " + hp);
                if (hp <= 0)
                {
                    dead = true;
                    animator.SetBool("dead", true);
                    deadTimer = deadTime;
                    respawnTimer = respawnTime;
                }
            }
            if (collision.gameObject.tag == "StunToEnemy")
            {
                stunTimer = timeStunned;
            }
        }
    }

    //Wenn der Player im Detection-Trigger ist, wird er aktiv und die Richtung festgelegt.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            active = true;
            if (other.transform.position.x + dist < transform.position.x)
            {
                dir = -1;

            }
            else if (other.transform.position.x - dist > transform.position.x)
            {
                dir = 1;
            }
            else
            {
                dir = 0;
            }
        }
    }

    //Wenn Spieler nicht mehr in Reichweite wird er deaktiviert
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            active = false;
        }
    }

    //Check ob der Gegner am Boden ist oder nicht.
    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckIfGrounded();
    }

    //Setzt grounded auf false beim verlassen des Colliders
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        animator.SetBool("Grounded", false);
    }

    //GroundedCheck
    void CheckIfGrounded()
    {
        grounded = false;
        animator.SetBool("Grounded", false);
        RaycastHit2D[] hits;

        //Überprüft, ob Grounded an der rechten Ecke des Gegners
        hits = Physics2D.RaycastAll(new Vector2(transform.position.x + halfSize - 0.15f, transform.position.y), new Vector2(0, -1), halfSize + 0.01f);

        if (hits.Length > 0)
        {
            grounded = true;
            animator.SetBool("Grounded", true);
        }
        else
        {
            //Falls die rechte Ecke nicht Gegrounded ist, wird die linke geprüft
            hits = Physics2D.RaycastAll(new Vector2(transform.position.x - halfSize + 0.15f, transform.position.y), new Vector2(0, -1), halfSize + 0.01f);

            if (hits.Length > 0)
            {
                grounded = true;
                animator.SetBool("Grounded", true);
            }
        }

        RaycastHit2D[] hitsRight;

        //Überprüft, ob rechts an der rechten Ecke des Gegners ein Block ist
        hitsRight = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y - halfSize + 0.15f), new Vector2(1*dir, 0), halfSize + 0.01f);

        jumpUp = 1;

        if (hitsRight.Length == 0)
        {
            rightUp = true;
        }
        else
        {
            if (grounded)
            {
                jumpUp = 0;
            }
        }


        /*
        //DEBUGGING DER RAYCASTS FÜR GROUNDED!
        foreach (RaycastHit2D hit in hitsRight)
        {
            GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            marker.transform.position = hit.point;
            marker.transform.localScale = Vector3.one * 0.1f;
            Destroy(marker, 0.1f);
        }
        */
    }
}
