using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoetryBuffDisplay : MonoBehaviour
{
    ParticleSystem particleSystem;

    PlayerStats playerStats;

	// Use this for initialization
	void Start ()
    {
        particleSystem = GetComponent<ParticleSystem>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(playerStats.poetryBuff>0)
        {
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
        }
	}
}
