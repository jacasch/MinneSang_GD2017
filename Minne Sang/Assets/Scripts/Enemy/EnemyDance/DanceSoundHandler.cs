using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceSoundHandler : MonoBehaviour
{

    public AudioClip[] dancingSound;
    public AudioClip[] enflameSound;
    public AudioClip[] explodingSound;


    private int lastDancingSound = 0;
    private int lastEnflameSound = 0;
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
    public void Dancing()
    {
        if(dancingSound.Length != 0)
        {
            lastDancingSound = PlaySound(dancingSound, lastDancingSound);
        }
    }

    public void Enflame()
    {
        if (enflameSound.Length != 0)
        {
            lastEnflameSound = PlaySound(enflameSound, lastEnflameSound);
        }
    }

    public void Exploding()
    {
        if (explodingSound.Length != 0)
        {
            lastExplodingSound = PlaySound(explodingSound, lastExplodingSound);
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