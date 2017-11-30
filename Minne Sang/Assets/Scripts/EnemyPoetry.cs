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
    float dmg = 0.25f;
    float speed = 0.5f;
    int dir = 0;
    float dist = 0;
    bool dead = false;

    // Use this for initialization
    void Start ()
    {
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
        else
        {
            Move();
        }
    }

    //Movement des Gegners
    void Move()
    {
        transform.Translate(speed * dir * Time.deltaTime, 0, 0);
    }

    //Tod des Gegners
    void Die()
    {
        //DieAnimation
        //...
        //Destroy Object
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
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

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            dir = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //DMG
        }
    }


}
