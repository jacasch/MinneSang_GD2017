using System.Collections;
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

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' oder 'Fear' beherscht.
    public bool stealth = false;
    public bool fear = false;
    int fearRadius = 10;

    //Eigenschaften des Gegners. (DMG ist untergeordnet in OozeDMG festgelegt!)
    bool active = false;
    int hp = 2;
    int speed = 5;
    int jumpHeight = 7;  //Sprunghöhe
    float jumpTimer = 0;
    float jumpCD = 0.2f;  //Zeit bis der Sprung nach der Landung erneut ausgeführt wird.
    int dir = 0;
    float dist = 0.5f;  //Distanz ab welcher der Gegner stillsteht(X-Achse).
    bool dead = false;

    //ScriptVariables
    bool grounded = false;
    Rigidbody2D rb;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //IF STEALTH, LOWER ALPHA / CHOOSE OTHER SPRITE ...

        //FEAR, SHOW PARTICLE EFFECT ...
        //FEAR, ENABLE TIRGGER COLLIDER FOR FEAR ...
    }

    void Update()
    {
        if (dead)
        {
            Die();
        }
        else
        {
            Move();
        }
    }

    //FUNCTIONS------------------------------------------------------------------------------------------------------------
    //Movement des Gegners
    void Move()
    {
        if(grounded)
        {
            jumpTimer -= Time.deltaTime;
            if(jumpTimer<=0)
            {
                jumpTimer = jumpCD;
                rb.velocity = new Vector3(speed * dir, jumpHeight, 0);
            }
        }
    }

    //Wenn der Gegner tot ist
    void Die()
    {
        //DieAnimation
        //Destroy Object
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

    //Collision Exit im Box-Collider (Trigger)
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            active = false;
            dir = 0;
        }
    }

    //Check ob der Ooze am Boden ist oder nicht.
    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckIfGrounded();
    }

    //Setzt grounded auf false bei Exit des Colliders
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }

    //GroundedCheck
    void CheckIfGrounded()
    {
        RaycastHit2D[] hits;

        Vector2 positionToCheck = transform.position;
        hits = Physics2D.RaycastAll (positionToCheck, new Vector2(0, -1), 0.01f);

        if(hits.Length > 0)
        {
            grounded = true;
        }
    }
}
