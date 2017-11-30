using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDance : MonoBehaviour
{
    /*
    GegnerInfo:

    Enemy: Dancer
    Ability: NONE
    Movement: Läuft dem Spieler nach, wird immer schneller.
    Attack: Explodiert bei Kollision mit dem Player oder wenn er stirbt.
    */

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' oder 'Fear' beherscht.
    public bool stealth = false;
    public bool fear = false;
    int fearRadius = 10;

    //Eigenschaften des Gegners.
    int hp = 1;
    float dmg = 1.5f;
    float speed = 0;
    float addSpeed = 0.05f;
    int maxSpeed = 8;
    int dir = 0;
    float dist = 0.5f;
    bool dead = false;
    float deadTimer = 0;
    float deadExpl = 0.5f;
    float deadEnd = 1f;

    //GameObjekt Explusion
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        //StealthShader
        //IF FEAR, SHOW PARTICLE EFFECT ...
        //IF FEAR, ENABLE TIRGGER COLLIDER FOR FEAR ...
    }

    // Update is called once per frame
    void Update()
    {
        if(dead)
        {
            deadTimer += Time.deltaTime;
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
        transform.Translate(speed*dir*Time.deltaTime, 0, 0);
    }

    //Tod des Gegners
    void Die()
    {
        if(deadTimer<deadExpl)
        {
            //PrepareExplusionAnimation

        } else
        {
            print("Boom");

            //Instanciate DanceExplusion -> DanceExplusion.dmg = dmg;
            Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(gameObject);

            if (deadTimer > deadEnd)
            {
                Destroy(gameObject);
            }
        }
    }

    //Collision Stay im Circle-Collider (Trigger)
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (speed < maxSpeed)
            {
                speed += addSpeed;
            }
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

    //Collision Exit im Circle-Collider (Trigger)
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            speed = 0;
            dir = 0;
        }
    }

    //Collision Enter im Cube-Collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            dead = true;
        }
    }
}