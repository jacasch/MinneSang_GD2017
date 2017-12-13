using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDanceExplosion : MonoBehaviour
{
    float timer = 0.5f;
    int timeUntilDestroyed = 1;

    CircleCollider2D bombColl;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start ()
    {
        gameObject.layer = 0;
        bombColl = GetComponent<CircleCollider2D>();
	}
	
	void Update ()
    {
        timer -= Time.deltaTime;
        if(timer<0)
        {
            bombColl.enabled = false;
        }
        if(timer < -timeUntilDestroyed)
        {
            Destroy(gameObject);
        }
	}
}
