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
    float speed = 0.5f;
    int dir = 0;
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
        Move();

        //Wenn der Gegner keine hp mehr hat oder tot ist, stirbt er
        if (dead)
        {
            Die();
        }
    }

    //Movement des Gegners
    void Move()
    {
        //Move
    }

    //Tod des Gegners
    void Die()
    {
        //DieAnimation
        //...
        //Destroy Object
    }





}
