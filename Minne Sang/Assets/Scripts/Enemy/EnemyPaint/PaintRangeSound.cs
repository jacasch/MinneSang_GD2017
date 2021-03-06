﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintRangeSound : MonoBehaviour
{

    public AudioClip[] idleSound;

    private int lastIdleSound = 0;

    private AudioSource audioSource;

    GameObject player;
    float dist = 100;
    float soundDist = 15;
    float soundScale = 5;
    bool isSound = false;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y));
        if (dist < soundDist)
        {
            if (!isSound)
            {
                Idle();
                isSound = true;
            }
            if (dist > soundDist - soundScale)
            {
                audioSource.volume = (soundDist - dist) / soundScale;
            }
            else
            {
                audioSource.volume = 1;
            }
        }
        else
        {
            isSound = false;
            audioSource.Stop();
        }
    }


    #region public funcions
    public void Idle()
    {
        if (idleSound.Length != 0)
        {
            lastIdleSound = PlaySound(idleSound, lastIdleSound);
        }
    }
    #endregion

    private int PlaySound(AudioClip[] sound, int lastPlayedIndex)
    {
        if (sound[0] == null)
        {
            Debug.Log("exit");
            return 0;
        }

        int newSound = lastPlayedIndex;

        if (sound.Length == 1)
        {
            newSound = 0;
        }
        else
        {
            while (newSound == lastPlayedIndex)
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