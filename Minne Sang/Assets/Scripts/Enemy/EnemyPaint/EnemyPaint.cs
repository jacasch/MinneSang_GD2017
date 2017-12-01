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
    float dist = 8;
    float shootTimer = 0;
    float shootCD = 2;
    bool dead = false;

    //Player GameObject
    GameObject objPlayer;

    void Start ()
    {
        //STEALTH, LOWER ALPHA / CHOOSE OTHER SPRITE ...

        //IF FEAR, SHOW PARTICLE EFFECT ...
        //IF FEAR, ENABLE TIRGGER COLLIDER FOR FEAR ...
    }

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

    //Save player as variable
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            objPlayer = collision.gameObject;
        }
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
}
