using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMusicStomp : MonoBehaviour
{
    float liveTime = 0.5f;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
