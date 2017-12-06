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

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' beherscht.
    public bool stealth = false;

    //Eigenschaften des Gegners. (DMG ist untergeordnet in OozeDMG festgelegt!)
    int hp = 2;  //HP des Gegners
    int speed = 5;  //Geschwindigkeit des Gegners
    int jumpHeight = 7;  //Sprunghöhe
    float jumpCD = 0.2f;  //Zeit bis der Sprung nach der Landung erneut ausgeführt wird
    float dist = 0.5f;  //Distanz ab welcher der Gegner stillsteht(X-Achse)
    float timeStunned = 3;

    //ScriptVariables
    bool active = false;
    bool grounded = false;
    float jumpTimer = 0;
    int dir = 0;
    float stunTimer = 0;
    float deadTimer = 1;
    bool dead = false;
    Rigidbody2D rb;
    SpriteRenderer mySprite;


    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();

        //IF STEALTH, LOWER ALPHA / CHOOSE OTHER SPRITE ...
    }

    void Update()
    {
        if (dead)
        {
            Die();
        }
        else if (stunTimer > 0)
        {
            //Stun Animation
            stunTimer -= Time.deltaTime;
        }
        else if(active)
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
        if (dir < 0)
        {
            mySprite.flipX = true;
        }
        else
        {
            mySprite.flipX = false;
        }
    }

    //Wenn der Gegner tot ist
    void Die()
    {
        deadTimer -= Time.deltaTime;
        //DieAnimation
        //...

        if (deadTimer < 0)
        {
            Destroy(gameObject);
        }
        //Destroy Object
    }

    //Wenn der Player den Gegner angreift
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DmgToEnemy")
        {
            hp -= 1;
            print("ENEMY HP: " + hp);
            if (hp<=0)
            {
                dead = true;
            }
        }
        if (collision.gameObject.tag == "StunToEnemy")
        {
            stunTimer = timeStunned;
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
