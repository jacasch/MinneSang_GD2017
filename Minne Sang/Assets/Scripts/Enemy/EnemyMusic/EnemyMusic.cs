using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMusic : MonoBehaviour
{
    /*
    GegnerInfo:

    Enemy: Oger
    Ability: Knockback -> Attacke erzeugt einen Knockback, kann angegriffen werden wenn betäubt.
    Movement: Läuft langsam in die Richtung des Spielers.
    Attack: Stampft beim gehen auf den Boden, Schaden und Knockback durch Druckwelle.
    */

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' oder 'Fear' beherscht.
    public bool stealth = false;
    public bool fear = false;
    int fearRadius = 10;

    //Eigenschaften des Gegners.
    int hp = 4;
    int speed = 1;
    int dir = 0;
    float dist = 1.5f;
    float walkTimer = 0;
    float walkDist = 0.5f;
    float walkCD = 1;
    bool dead = false;

    // Use this for initialization
    void Start ()
    {
        //IF STEALTH, LOWER ALPHA / CHOOSE OTHER SPRITE ...
        //IF FEAR, SHOW PARTICLE EFFECT ...
        //IF FEAR, ENABLE TIRGGER COLLIDER FOR FEAR ...
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
        walkTimer += Time.deltaTime;
        if (walkTimer < walkDist)
        {
            transform.Translate(speed * dir * Time.deltaTime, 0, 0);
        }
        else if (walkTimer > walkDist + walkCD)
        {
            walkTimer = 0;
        }
    }

    //Attacke des Gegners
    void Attack()
    {

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
