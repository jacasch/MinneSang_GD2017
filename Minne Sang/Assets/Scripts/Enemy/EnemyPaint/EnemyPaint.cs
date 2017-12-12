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

    //Eigenschaften des Gegners. (DMG ist untergeordnet im DMG Objekt und im EnemyPaintShot festgelegt!)
    int hpMax = 2;  //MAX HP des Gegners
    int speed = 2;  //Speed des Gegners
    float dist = 5;  //Distanz ab welcher der Gegner stillsteht(X-Achse)
    float shootCD = 1.5f;  //Cooldown des Schusses
    float timeStunned = 2f;  //Zeit die der Gegner gestunnt ist
    float deadTime = 0.75f;  //Zeit bis der Gegner nach dem Tot verschwindet
    float respawnTime = 30;  //Zeit bis der Gegner respawnt

    //Questereignisse
    string activeQuest;
    public bool dropItem = false;
    public string questName = "";
    public Item questDrop;
    bool dropped = false;

    //ScriptVariables
    int hp;  //HP des Gegners

    bool active = false;
    bool move = false;
    public bool stealth = true;
    bool dead = false;

    public int dir = 1;

    public float shootTimer = 0;
    float stunTimer = 0;
    float gotDmgTimer = 0;
    float deadTimer = 0;
    float respawnTimer = 0;

    Vector3 orgPos;
    Vector3 deadPos = new Vector3(1000, 0, 0);
    Rigidbody2D rb;
    public GameObject objShot;
    GameObject objPlayer;

    SpriteRenderer mySprite;
    public Material defaultMat;
    public Material chameleonMat;

    public EnemyDMG enemyDmg;
    public GameObject aura;
    EnemyDMG auraDmg;

    Animator animator;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start ()
    {
        hp = hpMax;
        orgPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        activeQuest = objPlayer.GetComponent<PlayerQuestHandler>().activeQuest;
        mySprite = GetComponent<SpriteRenderer>();
        auraDmg = aura.GetComponent<EnemyDMG>();

        animator = GetComponent<Animator>();

        if (stealth)
        {
            mySprite.material = chameleonMat;
        }
        else
        {
            mySprite.material = defaultMat;
        }
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
            if (dir > 0 && !stealth)
            {
                mySprite.flipX = true;
            }
            else
            {
                mySprite.flipX = false;
            }
            Attack();
        }
        if (shootTimer >= 0)
        {
            shootTimer -= Time.deltaTime;
        }
        if(gotDmgTimer>0)
        {
            gotDmgTimer -= Time.deltaTime;
            if (gotDmgTimer <= 0)
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
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
            GameObject instance = Instantiate(objShot, new Vector2(objPos.x + (0.65f * dir),objPos.y-0.65f), transform.rotation) as GameObject;
            instance.GetComponent<EnemyPaintShot>().direction = new Vector3(dirX, dirY, 0);
            instance.layer = 0;
            shootTimer = shootCD;
            animator.SetBool("Shooting", false);
        }
        else if(shootTimer < 0.1)
        {
            animator.SetBool("Shooting", true);
        }
    }

    //Tod des Gegners
    void Die()
    {
        enemyDmg.noDmg = true;
        auraDmg.noDmg = true;

        if (deadTimer > 0)
        {
            deadTimer -= Time.deltaTime;
            //DieAnimation
            //...
        }
        else
        {
            if (dropItem && !dropped)
            {
                if (activeQuest == questName)
                {
                    print("test");
                    GameObject drop = Instantiate(questDrop.drop, transform.position, transform.rotation);
                    drop.GetComponent<ItemHandler>().SetName(questDrop.name);
                    dropped = true;
                }
            }
            if (respawnTimer == respawnTime)
            {
                transform.position = deadPos;
            }
            if (respawnTimer < 0)
            {
                hp = hpMax;
                dropped = false;
                stealth = true;
                if (stealth)
                {
                    mySprite.material = chameleonMat;
                }
                dead = false;
                enemyDmg.noDmg = false;
                auraDmg.noDmg = false;
                transform.position = orgPos;
            }
            respawnTimer -= Time.deltaTime;
        }
        //Destroy Object
    }

    //Save player as variable
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //LÖSCHEN -> wird im Start ausgeführt.
        //if (collision.gameObject.tag == "Player")
        //{
        //    objPlayer = collision.gameObject;
        //}
        if (stealth)
        {
            if (collision.gameObject.tag == "Paint")
            {
                stealth = false;
                mySprite.material = defaultMat;
            }
        }
        else
        {
            if (collision.gameObject.tag == "DmgToEnemy")
            {
                hp -= 1;
                rb.velocity = new Vector3(4f * -dir, -1, 0);
                gotDmgTimer = 0.2f;
                print("ENEMY HP: " + hp);
                if (hp <= 0)
                {
                    dead = true;
                    deadTimer = deadTime;
                    respawnTimer = respawnTime;
                }
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
            if (other.transform.position.x + 0.75f < transform.position.x)
            {
                dir = -1;
            }
            else if (other.transform.position.x - 0.75f > transform.position.x)
            {
                dir = 1;
            }
            if(other.transform.position.x + dist < transform.position.x || other.transform.position.x - dist > transform.position.x)
            {
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
