using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDanceExplosion : MonoBehaviour
{
    float timer = 0.5f;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start ()
    {
        gameObject.layer = 0;
	}
	
	void Update ()
    {
        timer -= Time.deltaTime;
        if(timer<0)
        {
            Destroy(gameObject);
        }
	}
}
