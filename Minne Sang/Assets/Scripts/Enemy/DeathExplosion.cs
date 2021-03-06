﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplosion : MonoBehaviour
{
    public bool died = false;

    ParticleSystem particleSystem1;
    ParticleSystem particleSystem2;

    AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
        particleSystem1 = transform.GetChild(0).GetComponent<ParticleSystem>();
        particleSystem2 = transform.GetChild(1).GetComponent<ParticleSystem>();

        if (GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(died)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            particleSystem1.Play();
            particleSystem2.Play();
            died = false;
        }
    }
}
