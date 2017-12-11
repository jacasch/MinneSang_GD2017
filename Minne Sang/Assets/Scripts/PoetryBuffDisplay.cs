using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PoetryBuffDisplay : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.EmissionModule em;

    PlayerStats playerStats;
    float buff;

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
        buff = playerStats.poetryBuff;
		if(buff > 0)
        {
            ps.Play();
            if(buff<5)
            {
                em.rateOverTime = buff * 20;
            }
            else
            {
                em.rateOverTime = 100;
            }
        }
        else
        {
            ps.Stop();
        }
	}
}
