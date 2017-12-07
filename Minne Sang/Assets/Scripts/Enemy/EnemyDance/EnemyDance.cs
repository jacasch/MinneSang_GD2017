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

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' beherscht.
    public bool stealth = false;

    //Eigenschaften des Gegners.
    float speed = 0;  //Wirdd im Script laufend erhöht
    float addSpeed = 0.05f;  //Erhöhung des Speeds
    int maxSpeed = 8;  //Maximaler Speed
    float dist = 0.5f;  //Distanz ab welcher der Gegner stillsteht (X-Achse)
    float deadTimer = 1;  //Zeit Bis der Gegner verschwindet
    float deadExpl = 0.5f;  //Zeit bis der Gegner explosion erzeugt (deadTimer - deadExpl = Effektive Zeit)
    float timeStunned = 0.5f;

    //ScriptVariables
    bool active = false;
    bool move = false;
    int dir = 1;
    float stunTimer;
    bool dead = false;
    public GameObject explosion;
    SpriteRenderer mySprite;

    public Material defaultMat;
    public Material chameleonMat;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();

        if (stealth)
        {
            mySprite.material = chameleonMat;
        }
        else
        {
            mySprite.material = defaultMat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(dead)
        {
            deadTimer -= Time.deltaTime;
            Die();
        }
        else if(stunTimer>0)
        {
            //Stun Animation
            stunTimer -= Time.deltaTime;
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
        if (dir < 0 && !stealth)
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

    //Wenn der Player den Gegner angreift oder berührt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stealth)
        {
            if (collision.gameObject.tag == "Paint")
            {
                stealth = false;
                mySprite.material = defaultMat;
            }
            if (collision.gameObject.tag == "PlayerCollision")
            {
                dead = true;
            }
        }
        else
        {
            if (collision.gameObject.tag == "DmgToEnemy" || collision.gameObject.tag == "PlayerCollision")
            {
                dead = true;
            }
            if (collision.gameObject.tag == "StunToEnemy")
            {
                stunTimer = timeStunned;
            }
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