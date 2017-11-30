using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPaint : MonoBehaviour
{
    /*
    GegnerInfo:

    Enemy: Oktopus
    Ability: Stealth -> Unverwundbar solange nicht vom Spieler angemalt.
    Movement: Bewegt sich in Richtung des Spielers, bleibt aber auf Abstand.
    Attack: Schiesst auf den Spieler (Gerader, langsamer Schuss).
    */

    //Bestimmt, ob der Gegner die Fähigkeit 'Fear' beherscht.
    public bool fear = false;
    int fearRadius = 10;

    //Eigenschaften des Gegners.
    int hp = 2;
    int speed = 2;
    int dir = 0;
    float dist = 7;
    bool dead = false;

    // Use this for initialization
    void Start ()
    {
        //STEALTH, LOWER ALPHA / CHOOSE OTHER SPRITE ...

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
        transform.Translate(speed * dir * Time.deltaTime, 0, 0);
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

    private void OnTriggerStay2D(Collider2D other)
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

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            dir = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //DMG
        }
    }




}
