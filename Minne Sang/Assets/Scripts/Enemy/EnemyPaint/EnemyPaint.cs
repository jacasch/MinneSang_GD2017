using System.Collections;
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

    //Eigenschaften des Gegners.
    int hp = 2;  //HP des Gegners
    int speed = 2;  //Speed des Gegners
    float dist = 5;  //Distanz ab welcher der Gegner stillsteht(X-Achse)
    float shootCD = 1.5f;  //Cooldown des Schusses
    float timeStunned = 2f;

    //Player GameObject
    bool active = false;
    bool move = false;
    int dir = 1;
    float shootTimer = 0;
    float stunTimer = 0;
    float deadTimer = 1;
    bool stealth = true;
    bool dead = false;
    GameObject objPlayer;
    public GameObject objShot;
    SpriteRenderer mySprite;

    public Material defaultMat;
    public Material chameleonMat;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start ()
    {
        mySprite = GetComponent<SpriteRenderer>();

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
            if (dir < 0 && !stealth)
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
        deadTimer -= Time.deltaTime;
        //DieAnimation
        //...

        if (deadTimer < 0)
        {
            Destroy(gameObject);
        }
        //Destroy Object
    }

    //Save player as variable
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            objPlayer = collision.gameObject;
        }
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
                print("ENEMY HP: " + hp);
                if (hp <= 0)
                {
                    dead = true;
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
