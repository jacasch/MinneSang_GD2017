﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceSoundHandler : MonoBehaviour
{

    public AudioClip[] burningSound;
    public AudioClip[] explodingSound;


    private int lastBurningSound = 0;
    private int lastExplodingSound = 0;


    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    #region public funcions
    public void Burning()
    {
        lastBurningSound = PlaySound(burningSound, lastBurningSound);
    }

    public void Exploding()
    {
        lastExplodingSound = PlaySound(explodingSound, lastExplodingSound);
    }
    #endregion

    private int PlaySound(AudioClip[] sound, int lastPlayedIndex)
    {
        print(sound[0].name);
        if (sound[0] == null)
        {
            Debug.Log("exit");
            return 0;
        }

        int newSound = lastPlayedIndex;
        print("lastplayed: " + lastPlayedIndex);

        if (sound.Length == 1)
        {
            newSound = 0;
        }
        else
        {
            while(newSound == lastPlayedIndex)
            {
                newSound = (int)Random.Range(0, sound.Length);
            }
            lastPlayedIndex = newSound;
        }
        audioSource.clip = sound[lastPlayedIndex];
        audioSource.Play();

        return lastPlayedIndex;
    }
}