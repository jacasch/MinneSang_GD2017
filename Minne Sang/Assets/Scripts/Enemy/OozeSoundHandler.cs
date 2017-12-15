using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OozeSoundHandler : MonoBehaviour
{
    public AudioClip[] jumpSound;
    public AudioClip[] landSound;
    public AudioClip[] dyingSound;


    private int lastJumpSound = 0;
    private int lastLandSound = 0;
    private int lastDyingSound = 0;


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
    public void Jump()
    {
        if (jumpSound.Length != 0)
        {
            lastJumpSound = PlaySound(jumpSound, lastJumpSound);
        }
    }

    public void Land()
    {
        if (landSound.Length != 0)
        {
            lastLandSound = PlaySound(landSound, lastLandSound);
        }
    }

    public void Dying()
    {
        if (dyingSound.Length != 0)
        {
            lastDyingSound = PlaySound(dyingSound, lastDyingSound);
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