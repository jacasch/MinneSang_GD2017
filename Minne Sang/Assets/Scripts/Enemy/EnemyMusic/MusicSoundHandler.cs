using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSoundHandler : MonoBehaviour
{

    public AudioClip[] snoringSound;
    public AudioClip[] stompingSound;
    public AudioClip[] dyingSound;


    private int lastSnoringSound = 0;
    private int lastStompingSound = 0;
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
    public void Snoring()
    {
        if (snoringSound.Length != 0)
        {
            lastSnoringSound = PlaySound(snoringSound, lastSnoringSound);
        }
    }

    public void Stomping()
    {
        if (stompingSound.Length != 0)
        {
            lastStompingSound = PlaySound(stompingSound, lastStompingSound);
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