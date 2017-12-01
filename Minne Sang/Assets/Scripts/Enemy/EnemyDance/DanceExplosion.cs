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
}
