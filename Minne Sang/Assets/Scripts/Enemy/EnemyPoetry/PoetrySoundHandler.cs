using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoetrySoundHandler : MonoBehaviour
{
    public AudioClip[] dyingSound;
    public AudioClip[] screamingSound;


    private int lastDyingSound = 0;
    private int lastScreamingSound = 0;


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
    public void Dying()
    {
        if (dyingSound.Length != 0)
        {
            lastDyingSound = PlaySound(dyingSound, lastDyingSound);
        }
    }

    public void Screaming()
    {
        if (screamingSound.Length != 0)
        {
            lastScreamingSound = PlaySound(screamingSound, lastScreamingSound);
        }
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