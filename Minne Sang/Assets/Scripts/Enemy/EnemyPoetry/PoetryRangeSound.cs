using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoetryRangeSound : MonoBehaviour
{

    public AudioClip[] singingSound;
    public AudioClip[] screamingSound;

    private int lastSingingSound = 0;
    private int lastScreamingSound = 0;

    private AudioSource audioSource;

    GameObject player;
    float dist = 100;
    float soundDist = 12;
    float soundScale = 8;
    bool isSinging = false;
    bool isScreaming = false;
    public bool dead = false;

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
        if (dist < soundDist && !dead)
        {
            if (isScreaming)
            {
                if (isSinging)
                {
                    isSinging = false;
                    Screaming();
                }
            }
            else
            {
                if (!isSinging)
                {
                    Singing();
                    isSinging = true;
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
        }
        else
        {
            isSinging = false;
            audioSource.Stop();
        }
        if(dead)
        {
            audioSource.Stop();
        }
    }


    #region public funcions
    public void Singing()
    {
        if (singingSound.Length != 0)
        {
            lastSingingSound = PlaySound(singingSound, lastSingingSound);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isScreaming = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isScreaming = false;
        }
    }
}