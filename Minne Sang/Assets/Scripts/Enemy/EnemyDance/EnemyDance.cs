﻿using System.Collections;
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

    //Eigenschaften des Gegners. (DMG ist im Prefab Explosion festgelegt!)
    float startSpeed = 4.5f;  //Startgeschwindigkeit des Dancers
    float speed = 4.5f;  //Wirdd im Script laufend erhöht
    float addSpeed = 0.05f;  //Erhöhung des Speeds
    int maxSpeed = 9;  //Maximaler Speed
    float dist = 0.5f;  //Distanz ab welcher der Gegner stillsteht (X-Achse)
    float timeStunned = 0.5f;  //Zeit die der Gegner gestunnt ist wenn er gestunnt wird
    float deadTime = 0.5f;  //Zeit bis der Gegner nach dem Tot verschwindet
    float respawnTime = 90;  //Zeit bis der Gegner respawnt

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' beherscht.
    public bool isStealth = false;

    //Questereignisse
    PlayerQuestHandler questHandler;
    public bool dropItem = false;
    string questName = "q3";
    public Item questDrop;
    bool dropped = false;

    //ScriptVariables
    bool active = false;
    bool move = false;
    [HideInInspector]
    public bool stealth = false;
    [HideInInspector]
    public bool dead = false;
    bool exploded = false;
    bool respawning = false;
    bool isSound = false;

    int dir = 1;

    float stunTimer = 0;
    float deadTimer = 0;
    float respawnTimer = 0;


    Vector3 orgPos;
    Vector3 deadPos = new Vector3(1000, 0, 0);
    public GameObject explosion;

    Rigidbody2D rb;

    SpriteRenderer mySprite;
    public Material defaultMat;
    public Material chameleonMat;

    public GameObject aura;
    EnemyDMG auraDmg;

    Animator animator;

    DanceSoundHandler soundHandler;
    AudioSource audioSource;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start()
    {
        stealth = isStealth;
        orgPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        auraDmg = aura.GetComponent<EnemyDMG>();

        questHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuestHandler>();

        animator = GetComponent<Animator>();

        soundHandler = GetComponent<DanceSoundHandler>();
        audioSource = GetComponent<AudioSource>();

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
        animator.speed = speed - 3.5f;
        if (dead)
        {
            animator.speed = 1;
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
        else if(speed > startSpeed)
        {
            speed -= addSpeed;
        }
        else
        {
            speed = startSpeed;
            animator.SetBool("dance", false);
        }
        if(!dead)
        {
            audioSource.pitch = 1 + (((speed - startSpeed) / (maxSpeed - startSpeed)) * 2);
            if(speed == startSpeed)
            {
                audioSource.Stop();
                isSound = false;
            }
        }
    }

    //FUNCTIONS------------------------------------------------------------------------------------------------------------
    //Movement des Gegners
    void Move()
    {
        transform.Translate(speed*dir*Time.deltaTime, 0, 0);
        if (dir > 0)
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
        auraDmg.noDmg = true;
        animator.SetBool("explosion", true);
        animator.SetBool("dance", false);

        if (deadTimer>0)
        {
            deadTimer -= Time.deltaTime;
            audioSource.pitch = 1;
            if (!isSound)
            {
                soundHandler.Enflame();
                isSound = true;
            }
        }
        else
        {
            if(!exploded)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                audioSource.loop = false;
                soundHandler.Exploding();
                exploded = true;
            }

            foreach (string item in questHandler.collectedItems)
            {
                if (item == questDrop.name)
                {
                    dropped = true;
                }
            }

            if (dropItem && !dropped)
            {
                if (questHandler.activeQuest == questName)
                {
                    GameObject drop = Instantiate(questDrop.drop, transform.position, transform.rotation);
                    drop.GetComponent<ItemHandler>().SetName(questDrop.name);
                    dropped = true;
                }
            }
            if (respawnTimer == respawnTime)
            {
                transform.position = deadPos;
                mySprite.enabled = false;
            }
            if (respawnTimer < 0)
            {
                respawning = true;
                respawn();
            }
            respawnTimer -= Time.deltaTime;
        }
    }

    void respawn()
    {
        rb.velocity = new Vector3(0, 0, 0);
        transform.position = orgPos;
        if (respawnTimer < -0.25f)
        {
            exploded = false;
            animator.SetBool("explosion", false);
            dropped = false;
            stealth = isStealth;
            if (stealth)
            {
                mySprite.material = chameleonMat;
            }
            dead = false;
            animator.SetBool("dead", false);
            auraDmg.noDmg = false;
            respawning = false;
            isSound = false;
            mySprite.enabled = true;
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
                isSound = false;
                deadTimer = deadTime;
                respawnTimer = respawnTime;
            }
        }
        else
        {
            if (collision.gameObject.tag == "DmgToEnemy" || collision.gameObject.tag == "PlayerCollision")
            {
                dead = true;
                isSound = false;
                deadTimer = deadTime;
                respawnTimer = respawnTime;
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
            if (respawning)
            {
                transform.position = deadPos;
                respawnTimer = 10;
            }
            else if(!dead)
            {
                active = true;
                animator.SetBool("dance", true);
                if (speed < maxSpeed)
                {
                    speed += addSpeed;
                    animator.SetBool("dance", true);
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
                if (!isSound && !dead)
                {
                    audioSource.loop = true;
                    audioSource.pitch = 1;
                    soundHandler.Dancing();
                    isSound = true;
                }
            }
        }
    }

    //Wenn Spieler nicht mehr in Reichweite wird er deaktiviert und Speed auf 0 gesetzt
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            active = false;
        }
    }
}