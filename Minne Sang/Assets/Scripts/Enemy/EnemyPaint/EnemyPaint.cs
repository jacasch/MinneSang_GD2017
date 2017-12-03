﻿using System.Collections;
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
    int hp = 2;  //HP des Gegners
    int speed = 2;  //Speed des Gegners
    float dist = 8;  //Distanz ab welcher der Gegner stillsteht(X-Achse)
    float shootCD = 1.5f;  //Cooldown des Schusses

    //Player GameObject
    bool active = false;
    bool move = false;
    int dir = 1;
    float shootTimer = 0;
    bool dead = false;
    GameObject objPlayer;
    public GameObject objShot;

    //MAIN-----------------------------------------------------------------------------------------------------------------
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
        else if(active)
        {
            if(move)
            {
                Move();
            }
            Attack();
        }
        if (shootTimer >= 0)
        {
            shootTimer -= Time.deltaTime;
        }
    }

    //FUNCTIONS------------------------------------------------------------------------------------------------------------
    //Movement des Gegners
    void Move()
    {
        transform.Translate(speed * dir * Time.deltaTime, 0, 0);
    }

    //Attacke des Gegners
    void Attack()
    {
        if(shootTimer < 0)
        {
            Vector3 objPos = transform.position;
            float dirX = objPlayer.transform.position.x - transform.position.x;
            float dirY = objPlayer.transform.position.y - transform.position.y;;
            GameObject instance = Instantiate(objShot, objPos, transform.rotation) as GameObject;
            instance.GetComponent<EnemyPaintShot>().direction = new Vector3(dirX, dirY, 0);
            instance.layer = 0;
            shootTimer = shootCD;
        }
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

    //Wenn der Player im Detection-Trigger ist, wird er aktiv und die Richtung festgelegt.
    private void OnTriggerStay2D(Collider2D other)
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
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            active = false;
        }
    }
}
