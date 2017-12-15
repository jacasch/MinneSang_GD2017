using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    public AudioClip[] hittingSound;


    private int lastHittingSound = 0;


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
    public void Hitting()
    {
        if (hittingSound.Length != 0)
        {
            lastHittingSound = PlaySound(hittingSound, lastHittingSound);
        }
    }
    #endregion

    private int PlaySound(AudioClip[] sound, int lastPlayedIndex)
    {
        if (sound[0] == null)
        {
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