using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceExplosion : MonoBehaviour
{
    float timer = 0.5f;

	// Use this for initialization
	void Start ()
    {
        gameObject.layer = 0;
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
<<<<<<< HEAD:Minne Sang/Assets/Scripts/DanceExplosion.cs

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStats stats = collision.gameObject.GetComponent<PlayerStats>();
            if (stats.dmgTimer >= stats.dmgCD)
            {
                stats.dmgTimer = 0;
                stats.hp -= dmg;
            }
        }
    }
=======
>>>>>>> 8a0640239f6748a621bc9100720438da5e90377c:Minne Sang/Assets/Scripts/Enemy/EnemyDance/DanceExplosion.cs
}
