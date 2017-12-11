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

    //Eigenschaften des Gegners. (DMG ist untergeordnet im DMG Objekt und im Prefab EnemyPoetryAura festgelegt!)
    int hpMax = 3;  //MAX HP des Gegners
    float speed = 0.5f;  //Geschwindigkeit des Gegners
    float dist = 0;  //Distanz ab welcher der Gegner stillsteht(X-Achse)
    float timeStunned = 3f;  //Zeit die der Gegner gestunnt ist
    float deadTime = 1;  //Zeit bis der Gegner nach dem Tot verschwindet
    float respawnTime = 30;  //Zeit bis der Gegner respawnt

    //Bestimmt, ob der Gegner die Fähigkeit 'Stealth' oder 'Fear' beherscht.
    public bool isStealth = false;

    //ScriptVariables
    int hp;  //HP des Gegners

    bool move = false;
    bool stealth = false;
    bool dead = false;

    int dir = 0;

    float stunTimer = 0;
    float deadTimer = 0;
    float respawnTimer = 0;

    Vector3 orgPos;
    Vector3 deadPos = new Vector3(1000, 0, 0);

    SpriteRenderer mySprite;
    public Material defaultMat;
    public Material chameleonMat;

    public EnemyDMG enemyDmg;
    public GameObject aura;
    EnemyDMG auraDmg;

    // Use this for initialization
    void Start ()
    {
        hp = hpMax;
        stealth = isStealth;
        orgPos = transform.position;
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

    // Update is called once per frame
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
        else
        {
            if (move)
            {
                Move();
            }
        }
    }

    //Movement des Gegners
    void Move()
    {
        transform.Translate(speed * dir * Time.deltaTime, 0, 0);
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
                print("POETRY HP: " + hp);
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

    //Bewegungsrichtung wenn Player erkannt wird
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
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


    //Wechselt bewegung auf false wenn der Player aus der Sichtweite ist
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            move = false;
        }
    }
}
