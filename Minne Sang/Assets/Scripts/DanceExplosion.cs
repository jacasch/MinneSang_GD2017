using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceExplosion : MonoBehaviour
{
    float dmg;
    float timer = 0.5f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;
        if(timer<0)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();
            if (stats.dmgTimer >= stats.dmgCD)
            {
                stats.dmgTimer = 0;
                stats.hp -= dmg;
            }
        }
    }
}
