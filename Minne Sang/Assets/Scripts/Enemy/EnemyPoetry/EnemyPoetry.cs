using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoetry : MonoBehaviour
{
    /*
    GegnerInfo:

    Enemy: Sirene
    Ability: Fear -> Der Spieler wird verängstigt wenn er zu nahe ist und erleidet psychischen Schaden über Zeit, er kann sich die Angst nehmen mit Poetry.
    Movement: Ganz langsame Bewegung von A nach B.
    Attack: Fear
    */

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' oder 'Fear' beherscht.
    public bool stealth = false;
    int fearRadius = 10;

    //Eigenschaften des Gegners.
    int hp = 3;
    float speed = 0.5f;
    int dir = 0;
    float dist = 0;
    float stunTimer = 0;
    float timeStunned = 3f;
    bool move = false;
    float deadTimer = 1;
    bool dead = false;
    SpriteRenderer mySprite;

    // Use this for initialization
    void Start ()
    {
        mySprite = GetComponent<SpriteRenderer>();

        //IF STEALTH, LOWER ALPHA / CHOOSE OTHER SPRITE ...

        //FEAR, SHOW PARTICLE EFFECT ...
        //FEAR, ENABLE TIRGGER COLLIDER FOR FEAR ...
    }

    // Update is called once per frame
    void Update ()
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
        else
        {
            if (move)
            {
                Move();
            }
        }
    }

    //Movement des Gegners
    void Move()
    {
        transform.Translate(speed * dir * Time.deltaTime, 0, 0);
        if (dir < 0)
        {
            mySprite.flipX = true;
        }
        else
        {
            mySprite.flipX = false;
        }
    }

    //Tod des Gegners
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
            print("POETRY HP: " + hp);
            if (hp <= 0)
            {
                dead = true;
            }
        }
        if (collision.gameObject.tag == "StunToEnemy")
        {
            stunTimer = timeStunned;
        }
    }

    //Bewegungsrichtung wenn Player erkannt wird
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.transform.position.x + dist < transform.position.x)
            {
                dir = -1;
                move = true;
            }
            else if (other.transform.position.x - dist > transform.position.x)
            {
                dir = 1;
                move = true;
            }
            else
            {
                move = false;
            }
        }
    }


    //Wechselt bewegung auf false wenn der Player aus der Sichtweite ist
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            move = false;
        }
    }
}
