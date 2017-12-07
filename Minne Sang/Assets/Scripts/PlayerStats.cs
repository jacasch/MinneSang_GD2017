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


    void Start ()
    {
    }

    void Update()
    {
        if (dead)
        {
            //WHAT-EVER...
        }
        else
        {
            if (dmgTimer>0)
            {
                dmgTimer -= Time.deltaTime;
            }
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
    }
}
