using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Eigenschaften des Players
    public float maxHP = 7;
    public float hp = 7;
    bool dead = false;

    //Zeit bis erneut verwundbar nach eingegangenem Schaden
    public float dmgTimer = 0;
    public float dmgCD = 0.5f;
    public float poetryTime = 10;
    public float poetryCD = 5;
    public float poetryCastTime = 3;
    public float poetryCasting = 0;
    public float poetryBuff = -10;
    PlayerGui playerGui;

    void Start ()
    {
        playerGui = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGui>();
    }

    void Update()
    {
        if (dead)
        {
            hp = maxHP;
            dead = false;
            GetComponent<PlayerController>().KnockBack(0);
            GetComponent<PlayerSpawnHandler>().Respawn();
        }
        else
        {
            if(hp<=0)
            {
                dead = true;
            }
            poetry();
            if (dmgTimer>0)
            {
                dmgTimer -= Time.deltaTime;
            }
        }
	}

    void poetry()
    {
        if(poetryBuff < -poetryCD)
        {
            if (Input.GetAxis("Poetry") != 0 && playerGui.skillLevel >= 4)
            {
                poetryCasting += Time.deltaTime;
                print(poetryCasting);
                if (poetryCasting >= poetryCastTime)
                {
                    poetryBuff = poetryTime;
                }
            }
            else
            {
                poetryCasting = 0;
            }
        }
        else
        {
            poetryBuff -= Time.deltaTime;
            print("poetryBuff:" + poetryBuff);
        }
    }

    //Wenn der Spieler im DMG des Gegners steht und er verwundbar ist, bekommt er Schaden und wird kurzzeitig unverwundbar
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (dmgTimer<=0 && collision.gameObject.tag == "DmgToPlayer")
        {
            //Variables

            EnemyDMG enemyDMG = collision.GetComponent<EnemyDMG>();
            int dir = 1;

            //Prüft ob Gegner bereits erneut Schaden verursachen kann
            if (enemyDMG.timer < 0)
            {
                //DMG to player
                hp -= enemyDMG.dmg;
                print("HP: " + hp);

                //UnverwundbarkeitsTimer
                dmgTimer = dmgCD;
                //print("Timer: " + dmgTimer);

                //Knockback
                if (collision.transform.position.x > transform.position.x)
                {
                    dir = -1;
                }
                GetComponent<PlayerController>().KnockBack(enemyDMG.knockback * dir);
                //print("Knockback: " + enemyDMG.knockback);

                //Timer bis Gegner erneut Schaden verursachen kann
                enemyDMG.timer = enemyDMG.dmgTime;
            }
        }
        if (dmgTimer <= 0 && poetryBuff <= 0 && collision.gameObject.tag == "PoetryDmgToPlayer")
        {
            //Variables
            EnemyDMG enemyDMG = collision.GetComponent<EnemyDMG>();
            int dir = 1;

            //Prüft ob Gegner bereits erneut Schaden verursachen kann
            if (enemyDMG.timer < 0)
            {
                //DMG to player
                hp -= enemyDMG.dmg;
                print("HP: " + hp);

                //UnverwundbarkeitsTimer
                dmgTimer = dmgCD;
                //print("Timer: " + dmgTimer);

                //Knockback
                if (collision.transform.position.x > transform.position.x)
                {
                    dir = -1;
                }
                GetComponent<PlayerController>().KnockBack(enemyDMG.knockback * dir);
                //print("Knockback: " + enemyDMG.knockback);

                //Timer bis Gegner erneut Schaden verursachen kann
                enemyDMG.timer = enemyDMG.dmgTime;
            }
        }
    }
}
