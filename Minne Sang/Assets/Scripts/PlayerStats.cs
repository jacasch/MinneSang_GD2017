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

	// Use this for initialization
	void Start ()
    {
        //....
	}

    // Update is called once per frame
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
