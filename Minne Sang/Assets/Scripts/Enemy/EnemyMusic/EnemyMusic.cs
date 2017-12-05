﻿using System.Collections;
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

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' beherscht.
    public bool stealth = false;

    //Eigenschaften des Gegners.
    int hp = 5;  //HP des Gegners
    int speed = 1;  //Speed des Gegners
    float dist = 1f;  //Distanz ab welcher der Gegner stillsteht(X-Achse)
    float walkDist = 0.5f;  //Zeit die der Gegner zwischen den Schritten sich vorwärtz bewegt
    float walkCD = 1f; //Zeit bis zum nächsten Schritt
    float timeStunned = 3f;

    //ScriptVariables
    bool active = false;
    bool move = false;
    int dir = 1;
    float walkTimer = 0;
    float stunTimer = 0;
    bool stomp = false;
    float deadTimer = 1;
    bool dead = false;
    public GameObject objStomp;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start ()
    {
        //IF STEALTH, LOWER ALPHA / CHOOSE OTHER SPRITE ...
    }

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
        else if(active)
        {
            if(move)
            {
                Move();
            }
            Attack();
        }
    }

    //FUNCTIONS------------------------------------------------------------------------------------------------------------
    //Movement des Gegners
    void Move()
    {
        if (walkTimer <= 0)
        {
            walkTimer = walkCD;
            transform.Translate(speed * dir * Time.deltaTime, 0, 0);
        }
        else if (walkTimer < walkDist)
        {
            transform.Translate(speed * dir * Time.deltaTime, 0, 0);
        }

        walkTimer -= Time.deltaTime;
        if (walkTimer <= 0)
        {
            stomp = true;
        }
    }

    //Attacke des Gegners
    void Attack()
    {
        if (stomp)
        {
            Vector3 objPos = transform.position;
            objPos.y -= 0f;
            GameObject instance = Instantiate(objStomp, objPos, transform.rotation) as GameObject;
            instance.layer = 0;
            stomp = false;
        }
    }

    //Tod des Gegners
    void Die()
    {
        deadTimer -= Time.deltaTime;
        //DieAnimation
        //...

        if(deadTimer<0)
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
            print("MUSIC HP: " + hp);
            if(hp<=0)
            {
                dead = true;
            }
        }
        if (collision.gameObject.tag == "StunToEnemy")
        {
            stunTimer = timeStunned;
        }
    }

    //Wenn der Player im Detection-Trigger ist, wird er aktiv und die Richtung festgelegt.
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            active = true;
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

    //Wenn Spieler nicht mehr in Reichweite wird er deaktiviert
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            active = false;
        }
    }
}
