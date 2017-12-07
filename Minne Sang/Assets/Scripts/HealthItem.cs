using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int hp = 10;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            PlayerStats stats = collision.GetComponent<PlayerStats>();
            stats.hp += hp;
            print(stats.hp);
            if(stats.hp > stats.maxHP)
            {
                stats.hp = stats.maxHP;
            }
            print(stats.hp);
            Destroy(gameObject);
        }
    }
}
