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


	void Start ()
    {
        //....
	}

    void Update()
    {
        if (dead)
        {
            //WHAT-EVER...
        }
        else
        {
            if(dmgTimer>0)
            {
                dmgTimer -= Time.deltaTime;
            }
        }
	}

    //Wenn der Spieler im DMG des Gegners steht und er verwundbar ist, bekommt er Schaden und wird kurzzeitig unverwundbar
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (dmgTimer<=0 && collision.gameObject.tag == "DMG")
        {
            hp -= collision.GetComponent<EnemyDMG>().dmg;
            print(hp);
            dmgTimer = dmgCD;
            print(dmgTimer);
        }
    }
}
