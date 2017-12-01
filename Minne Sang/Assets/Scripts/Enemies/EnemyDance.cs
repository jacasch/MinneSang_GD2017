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
    bool active = false;
    float speed = 0;
    float addSpeed = 0.05f;  //Beschleunigung des Speeds
    int maxSpeed = 8;
    int dir = 0;
    float dist = 0.5f;  //Distanz ab welcher der Gegner stillsteht (X-Achse)
    bool dead = false;
    float deadTimer = 1;  //Zeit Bis der Gegner verschwindet
    float deadExpl = 0.5f;  //Zeit bis der Gegner explosion erzeugt (deadTimer - deadExpl = Effektive Zeit)

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
            deadTimer -= Time.deltaTime;
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
        if(deadTimer>=deadExpl)
        {
            //PrepareExplusionAnimation

        } else
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            if (deadTimer > 0)
            {
                Destroy(gameObject);
            }
        }
    }

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DMG" || collision.gameObject.tag == "PlayerCollision")
        {
            dead = true;
        }

    }

    //Collision Stay im Circle-Collider (Trigger)
    private void OnTriggerStay2D(Collider2D other)
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
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            speed = 0;
            dir = 0;
        }
    }
}