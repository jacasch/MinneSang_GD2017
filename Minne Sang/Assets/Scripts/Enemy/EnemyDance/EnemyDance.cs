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
    float speed = 0;  //Wirdd im Script laufend erhöht
    float addSpeed = 0.05f;  //Erhöhung des Speeds
    int maxSpeed = 8;  //Maximaler Speed
    float dist = 0.5f;  //Distanz ab welcher der Gegner stillsteht (X-Achse)
    float deadTimer = 1;  //Zeit Bis der Gegner verschwindet
    float deadExpl = 0.5f;  //Zeit bis der Gegner explosion erzeugt (deadTimer - deadExpl = Effektive Zeit)

    //ScriptVariables
    bool active = false;
    bool move = false;
    int dir = 1;
    bool dead = false;
    public GameObject explosion;

    //MAIN-----------------------------------------------------------------------------------------------------------------
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
        else if(active)
        {
            if (move)
            {
                Move();
            }
        }
    }

    //FUNCTIONS------------------------------------------------------------------------------------------------------------
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

    //Wenn der Player den Gegner berührt oder angreift
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DmgToEnemy" || collision.gameObject.tag == "PlayerCollision")
        {
            dead = true;
        }

    }

    //Wenn der Player im Detection-Trigger ist, wird er aktiv und die Richtung festgelegt.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            active = true;
            if (speed < maxSpeed)
            {
                speed += addSpeed;
            }
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

    //Wenn Spieler nicht mehr in Reichweite wird er deaktiviert und Speed auf 0 gesetzt
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            active = false;
            speed = 0;
        }
    }
}