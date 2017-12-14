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

    //Eigenschaften des Gegners. (DMG ist untergeordnet im DMG Objekt und im EnemyPaintShot festgelegt!)
    int hpMax = 2;  //MAX HP des Gegners
    int speed = 2;  //Speed des Gegners
    float dist = 5;  //Distanz ab welcher der Gegner stillsteht(X-Achse)
    float shootCD = 1.5f;  //Cooldown des Schusses
    float timeStunned = 2f;  //Zeit die der Gegner gestunnt ist
    float deadTime = 0.75f;  //Zeit bis der Gegner nach dem Tot verschwindet
    float respawnTime = 90;  //Zeit bis der Gegner respawnt

    //Questereignisse
    string activeQuest;
    public bool dropItem = false;
    string questName = "q4";
    public Item questDrop;
    bool dropped = false;

    //ScriptVariables
    int hp;  //HP des Gegners

    bool active = false;
    bool move = false;
    [HideInInspector] public bool stealth = true;
    bool dead = false;
    bool died = false;
    bool respawning = false;
    bool wall = false;

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

    DeathExplosion deathExplosion;

    SpriteRenderer mouthSprite;

    PaintSoundHandler soundHandler;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start ()
    {
        hp = hpMax;
        orgPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        mySprite = GetComponent<SpriteRenderer>();
        auraDmg = aura.GetComponent<EnemyDMG>();

        stealth = true;

        deathExplosion = transform.Find("DeathExplosion").GetComponent<DeathExplosion>();

        animator = GetComponent<Animator>();

        soundHandler = GetComponent<PaintSoundHandler>();

        mouthSprite = transform.Find("Mouth").GetComponent<SpriteRenderer>();

        if (stealth)
        {
            mySprite.material = chameleonMat;
            mouthSprite.material = chameleonMat;
        }
        else
        {
            mySprite.material = defaultMat;
            mouthSprite.material = defaultMat;
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
            if(move && !wall)
            {
                Move();
            }
            if (dir > 0)
            {
                mySprite.flipX = true;
                mouthSprite.flipX = true;
            }
            else
            {
                mySprite.flipX = false;
                mouthSprite.flipX = false;
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

        if(!dead)
        {
            wall = false;
            RaycastHit2D[] hits;

            //Überprüft, ob Grounded an der rechten Ecke des Gegners
            hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y-0.8f), new Vector2(1*dir, 0), 0.9f);

            if (hits.Length > 0)
            {
                wall = true;
            }
            else
            {
                hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + 0.05f), new Vector2(1 * dir, 0), 0.9f);
                if (hits.Length > 0)
                {
                    wall = true;
                }
            }
            
            /*
            //DEBUGGING DER RAYCASTS FÜR GROUNDED!
            foreach (RaycastHit2D hit in hits)
            {
                GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                marker.transform.position = hit.point;
                marker.transform.localScale = Vector3.one * 0.1f;
                Destroy(marker, 0.1f);
            }
            */
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
            float dirY = objPlayer.transform.position.y - transform.position.y;
            soundHandler.Shoot();
            GameObject instance = Instantiate(objShot, new Vector2(objPos.x + (0.57f * dir),objPos.y-0.44f), transform.rotation) as GameObject;
            instance.GetComponent<EnemyPaintShot>().direction = new Vector3(dirX, dirY+0.4f, 0);
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

        if (died)
        {
            activeQuest = objPlayer.GetComponent<PlayerQuestHandler>().activeQuest;
            deathExplosion.died = true;
            soundHandler.Dying();
            died = false;
        }

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
                print("dropItem && dropped = True");
                print("activequeset: " + activeQuest + "|| questName: " + questName);
                if (activeQuest == questName)
                {
                    print("success!!!");
                    GameObject drop = Instantiate(questDrop.drop, transform.position, transform.rotation);
                    drop.GetComponent<ItemHandler>().SetName(questDrop.name);
                    dropped = true;
                }
            }
            if (respawnTimer == respawnTime)
            {
                transform.position = deadPos;
                mySprite.enabled = false;
                mouthSprite.enabled = false;
            }
            if (respawnTimer < 0)
            {
                respawning = true;
                respawn();
            }
            respawnTimer -= Time.deltaTime;
        }
        //Destroy Object
    }

    void respawn()
    {
        rb.velocity = new Vector3(0, 0, 0);
        transform.position = orgPos;
        if (respawnTimer < -0.25f)
        {
            hp = hpMax;
            dropped = false;
            stealth = true;
            if (stealth)
            {
                mySprite.material = chameleonMat;
                mouthSprite.material = chameleonMat;
            }
            dead = false;
            animator.SetBool("dead", false);
            enemyDmg.noDmg = false;
            auraDmg.noDmg = false;
            respawning = false;
            mySprite.enabled = true;
            mouthSprite.enabled = true;
        }
    }

    //Save player as variable
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dead)
        {
            if (stealth)
            {
                if (collision.gameObject.tag == "Paint")
                {
                    stealth = false;
                    mySprite.material = defaultMat;
                    mouthSprite.material = defaultMat;
                }
            }
            else
            {
                if (collision.gameObject.tag == "DmgToEnemy" && gotDmgTimer <= 0)
                {
                    hp -= 1;
                    //rb.velocity = new Vector3(4f * -dir, -1, 0);
                    gotDmgTimer = 0.2f;
                    if (hp <= 0)
                    {
                        dead = true;
                        died = true;
                        deadTimer = deadTime;
                        respawnTimer = respawnTime;
                    }
                    else
                    {
                        rb.velocity = new Vector3(4f * -dir, -1, 0);
                    }
                }
                if (collision.gameObject.tag == "StunToEnemy")
                {
                    stunTimer = timeStunned;
                }
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
            else
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
                if (other.transform.position.x + dist < transform.position.x || other.transform.position.x - dist > transform.position.x)
                {
                    move = true;
                }
                else
                {
                    move = false;
                }
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
