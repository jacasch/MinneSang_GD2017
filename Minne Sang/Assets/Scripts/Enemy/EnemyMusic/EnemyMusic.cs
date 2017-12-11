using System.Collections;
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

    //Eigenschaften des Gegners. (DMG ist im Prefab Stomp festgelegt!)
    int hpMax = 5;  //MAX HP des Gegners
    int speed = 1;  //Speed des Gegners
    float dist = 0.3f;  //Distanz ab welcher der Gegner stillsteht(X-Achse)
    float walkDist = 0.5f;  //Zeit die der Gegner zwischen den Schritten sich vorwärtz bewegt
    float walkCD = 1f; //Zeit bis zum nächsten Schritt
    float timeStunned = 3f;  //Zeit die der Gegner gestunnt ist
    float deadTime = 1;  //Zeit bis der Gegner nach dem Tot verschwindet
    float respawnTime = 30;  //Zeit bis der Gegner respawnt

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' beherscht.
    public bool isStealth = false;

    //ScriptVariables
    int hp;  //HP des Gegners

    bool active = false;
    bool move = false;
    bool stealth = false;
    bool stomp = false;
    bool dead = false;

    int dir = 1;

    float walkTimer = 0;
    float stunTimer = 0;
    float deadTimer = 0;
    float respawnTimer = 0;

    Vector3 orgPos;
    Vector3 deadPos = new Vector3(1000, 0, 0);
    public GameObject objStomp;

    Rigidbody2D rb;

    SpriteRenderer mySprite;
    public Material defaultMat;
    public Material chameleonMat;

    public EnemyDMG enemyDmg;
    public GameObject aura;
    EnemyDMG auraDmg;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start ()
    {
        hp = hpMax;
        stealth = isStealth;
        orgPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        auraDmg = aura.GetComponent<EnemyDMG>();

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
            walkTimer = 0;
        } else if(move)
        {
            Move();
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
            //transform.Translate(speed * dir * Time.deltaTime, 0, 0);
        }
        else if (walkTimer < walkDist)
        {
            transform.Translate(speed * dir * Time.deltaTime, 0, 0);
        }

        walkTimer -= Time.deltaTime;
        if (walkTimer <= 0)
        {
            stomp = true;
            move = false;
        }
        if (dir < 0 && !stealth)
        {
            mySprite.flipX = true;
        }
        else
        {
            mySprite.flipX = false;
        }
    }

    //Attacke des Gegners
    void Attack()
    {
        if (stomp)
        {
            Vector3 objPos = transform.position;
            objPos.y -= 0f;
            Instantiate(objStomp, objPos, transform.rotation);
            stomp = false;
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
            if (respawnTimer == respawnTime)
            {
                transform.position = deadPos;
            }
            if (respawnTimer < 0)
            {
                hp = hpMax;
                stealth = isStealth;
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

    //Wenn der Player den Gegner angreift
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
                print("MUSIC HP: " + hp);
                if (hp <= 0)
                {
                    dead = true;
                    deadTimer = deadTime;
                    respawnTimer = respawnTime;
                    walkTimer = 0;
                }
            }
            if (collision.gameObject.tag == "StunToEnemy")
            {
                stunTimer = timeStunned;
            }
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
                if(walkTimer > walkDist)
                {
                    dir = -1;
                }
                move = true;
            }
            else if (other.transform.position.x - dist > transform.position.x)
            {
                if (walkTimer > walkDist)
                {
                    dir = 1;
                }
                move = true;
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb.AddForce(new Vector2(collision.rigidbody.velocity.y * -1,0));
        }
    }
}
