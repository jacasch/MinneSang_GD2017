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

    //Eigenschaften des Gegners.
    int hp = 2;
    float dmg = 0.5f;
    int speed = 5;
    int jumpHeight = 0;
    int jumpMax = 7;
    float jumpTimer = 0;
    float jumpCD = 0.2f;
    int dir = 0;
    float dist = 0.5f;
    bool dead = false;

    //ScriptVariables
    bool grounded = false;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //IF STEALTH, LOWER ALPHA / CHOOSE OTHER SPRITE ...

        //FEAR, SHOW PARTICLE EFFECT ...
        //FEAR, ENABLE TIRGGER COLLIDER FOR FEAR ...
    }

    // Update is called once per frame
    void Update()
    {
        //Wenn der Gegner keine hp mehr hat oder tot ist, stirbt er
        if (dead)
        {
            Die();
        }
        else
        {
            Move();
        }
    }

    //Movement des Gegners
    void Move()
    {
        if(grounded)
        {
            jumpTimer += Time.deltaTime;
            if(jumpTimer>jumpCD)
            {
                jumpTimer = 0;
                rb.velocity = new Vector3(speed * dir, jumpHeight, 0);
            }
        }
    }

    //Tod des Gegners
    void Die()
    {
        //DieAnimation
        //Destroy Object
    }

    //Collision Stay im Box-Collider (Trigger)
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            jumpHeight = jumpMax;
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
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            jumpHeight = 0;
            dir = 0;
        }
    }

    //Collision Enter im Capsule-Collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();
            if (stats.dmgTimer >= stats.dmgCD)
            {
                stats.dmgTimer = 0;
                stats.hp -= dmg;
            }
        }
    }

    //Collision Stay im Capsule-Collider
    void OnCollisionStay2D(Collision2D collision)
    {
        CheckIfGrounded();
    }

    //Collision Exit im Capsule-Collider
    void OnCollisionExit2D(Collision2D collision)
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
