using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceRangeSound : MonoBehaviour
{

    public AudioClip[] burningSound;

    private int lastBurningSound = 0;

    private AudioSource audioSource;

    GameObject player;
    float dist = 100;
    float soundDist = 15;
    float soundScale = 7;
    bool isSound = false;

    EnemyDance enemyDance;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        player = GameObject.FindGameObjectWithTag("Player");

        enemyDance = transform.parent.GetComponent<EnemyDance>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y));
        if (dist < soundDist && !enemyDance.dead)
        {
            if (!isSound)
            {
                Burning();
                isSound = true;
            }
            if (dist > soundDist - soundScale)
            {
                audioSource.volume = (soundDist-dist)/soundScale;
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
    public void Burning()
    {
        if (burningSound.Length != 0)
        {
            lastBurningSound = PlaySound(burningSound, lastBurningSound);
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