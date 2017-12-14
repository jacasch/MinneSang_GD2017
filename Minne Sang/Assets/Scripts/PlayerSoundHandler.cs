using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundHandler : MonoBehaviour {

    public AudioClip[] attackSound;
    public AudioClip[] hitSound;
    public AudioClip[] dieSound;
    public AudioClip[] dashSound;
    public AudioClip[] poetryCastSound;
    public AudioClip[] stunSound;
    public AudioClip[] stepSound;

    private int lastAttackSound = 0;
    private int lastHitSound = 0;
    private int lastDieSound = 0;
    private int lastDashSound = 0;
    private int lastPoetryCastSound = 0;
    private int lastStunSound = 0;
    private int lastStepSound = 0;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    #region public funcions
    public void Attack() {
        lastAttackSound = PlaySound(attackSound, lastAttackSound);
    }

    public void Hit() {
        lastHitSound = PlaySound(hitSound, lastHitSound);
    }

    public void Die()
    {
        lastDieSound = PlaySound(dieSound, lastDieSound);
    }

    public void Dash() {
        lastDashSound = PlaySound(dashSound, lastDashSound);
    }

    public void CastPoetry() {
        lastPoetryCastSound = PlaySound(poetryCastSound, lastPoetryCastSound);
    }

    public void Stun() {
        lastStunSound = PlaySound(stunSound, lastStunSound);
    }

    public void Step() {
        lastStepSound = PlaySound(stepSound, lastStepSound);
    }
    #endregion

    private int PlaySound(AudioClip[] sound, int lastPlayedIndex) {
        if (sound[0] == null) {
            Debug.Log("exit");
            return 0;
        }

        int newSound = lastPlayedIndex;

        if (sound.Length == 1)
        {
            newSound = 0;
        }
        else {
            while (newSound == lastPlayedIndex)
            {
                newSound = (int)Random.Range(0, sound.Length - 1);
            }
            lastPlayedIndex = newSound;
        }
        audioSource.clip = sound[lastPlayedIndex];
        audioSource.Play();
        return lastPlayedIndex;
    }
}
