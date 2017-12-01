using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Eigenschaften des Players
    public float hp = 7;
    bool dead = false;

    //Zeit bis erneut verwundbar nach eingegangenem Schaden
    public float dmgTimer = 0;
    public float dmgCD = 1.5f;
    float knockbackTimer = 0;
    Rigidbody2D rb;


    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(knockbackTimer<0)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            knockbackTimer -= Time.deltaTime;
        }
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
            EnemyDMG enemyDMG = collision.GetComponent<EnemyDMG>();
            int dir = 1;
            if(collision.transform.position.x > transform.position.x)
            {
                dir = -1;
            }
            hp -= enemyDMG.dmg;
            print(hp);
            dmgTimer = dmgCD;
            print(dmgTimer);
            knockbackTimer = enemyDMG.knockback;
            rb.velocity = new Vector3(10*dir, 0, 0);
        }
    }
}
