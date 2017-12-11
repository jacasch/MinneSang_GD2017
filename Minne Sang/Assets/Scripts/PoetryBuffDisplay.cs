using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PoetryBuffDisplay : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.EmissionModule em;

    PlayerStats playerStats;

	// Use this for initialization
	void Start ()
    {
        ps = GetComponent<ParticleSystem>();
        em = ps.emission;

        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(playerStats.poetryBuff>0)
        {
            ps.Play();
            em.rateOverTime = playerStats.poetryBuff*10+20;
        }
        else
        {
            ps.Stop();
        }
	}
}
