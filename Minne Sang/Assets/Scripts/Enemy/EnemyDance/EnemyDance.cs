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

    //Eigenschaften des Gegners. (DMG ist im Prefab Explosion festgelegt!)
    float startSpeed = 4.5f;  //Startgeschwindigkeit des Dancers
    float speed = 4.5f;  //Wirdd im Script laufend erhöht
    float addSpeed = 0.05f;  //Erhöhung des Speeds
    int maxSpeed = 9;  //Maximaler Speed
    float dist = 0.5f;  //Distanz ab welcher der Gegner stillsteht (X-Achse)
    float timeStunned = 0.5f;  //Zeit die der Gegner gestunnt ist wenn er gestunnt wird
    float deadTime = 0.5f;  //Zeit bis der Gegner nach dem Tot verschwindet
    float respawnTime = 30;  //Zeit bis der Gegner respawnt

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' beherscht.
    public bool isStealth = false;

    //Questereignisse
    string activeQuest;
    public bool dropItem = false;
    public string questName = "";
    public Item questDrop;
    bool dropped = false;

    //ScriptVariables
    bool active = false;
    bool move = false;
    bool stealth = false;
    bool dead = false;
    bool exploded = false;

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

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start()
    {
        stealth = isStealth;
        orgPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        activeQuest = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuestHandler>().activeQuest;
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
    }

    //FUNCTIONS------------------------------------------------------------------------------------------------------------
    //Movement des Gegners
    void Move()
    {
        transform.Translate(speed*dir*Time.deltaTime, 0, 0);
        if (dir > 0 && !stealth)
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
            //PrepareExplusionAnimation
        }
        else
        {
            if(!exploded)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                exploded = true;
            }
            if(dropItem && !dropped)
            {
                if (activeQuest == questName)
                {
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
                exploded = false;
                animator.SetBool("explosion", false);
                dropped = false;
                stealth = isStealth;
                if (stealth)
                {
                    mySprite.material = chameleonMat;
                }
                dead = false;
                auraDmg.noDmg = false;
                rb.velocity = new Vector3(0, 0, 0);
                transform.position = orgPos;
            }
            respawnTimer -= Time.deltaTime;
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
                deadTimer = deadTime;
                respawnTimer = respawnTime;
            }
        }
        else
        {
            if (collision.gameObject.tag == "DmgToEnemy" || collision.gameObject.tag == "PlayerCollision")
            {
                dead = true;
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
        }
    }

    //Wenn Spieler nicht mehr in Reichweite wird er deaktiviert und Speed auf 0 gesetzt
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            active = false;
            //speed = startSpeed;
        }
    }
}