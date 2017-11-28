using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDance : MonoBehaviour
{
    /*
    GegnerInfo:

    Ability: None
    Movement: ...Läuft Spieler nach ... sonst was...?
    Attack: Explodiert bei Kollision mit dem Player oder wenn er stirbt.
    */

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' oder 'Fear' beherscht.
    public bool stealth = false;
    public bool fear = false;
    int fearRadius = 10;

    //Eigenschaften des Gegners.
    int hp = 1;
    int speed = 5;
    bool dead = false;


    // Use this for initialization
    void Start()
    {
        //IF STEALTH, LOWER ALPHA / CHOOSE OTHER SPRITE ...
        //IF FEAR, SHOW PARTICLE EFFECT ...
        //IF FEAR, ENABLE TIRGGER COLLIDER FOR FEAR ...
    }

    // Update is called once per frame
    void Update()
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
        //Explusion
        //DMG PLayer
        //Destroy Object
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dead = true;
        }
    }








}