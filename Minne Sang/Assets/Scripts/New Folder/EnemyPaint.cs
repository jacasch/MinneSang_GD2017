using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPaint : MonoBehaviour
{
    /*
    GegnerInfo:

    Ability: Stealth -> Unverwundbar solange nicht vom Spieler angemalt.
    Movement: ...
    Attack: ...
    */

    //Bestimmt, ob der Gegner die Fähigkeit 'Fear' beherscht.
    public bool fear = false;
    int fearRadius = 10;

    //Eigenschaften des Gegners.
    int hp = 2;
    int speed = 5;
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
