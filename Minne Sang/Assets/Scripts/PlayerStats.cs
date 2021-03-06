﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Eigenschaften des Players
    public float maxHP = 7;
    public float hp = 7;
    bool dead = false;

    //Zeit bis erneut verwundbar nach eingegangenem Schaden
    //public float dmgTimer = 0;
    public float dmgCD = 0.5f;
    public float poetryTime = 10;
    public float poetryCD = 5;
    public float poetryCastTime = 2;
    public float poetryCasting = 0;
    public float poetryBuff = -10;
    PlayerGui playerGui;
    Animator animator;

    private float deathtime;
    private bool deathAnimationStarted = false;
    private float respawnDelay = 2f;

    private bool playedPoetrySound = false;

    AudioSource asHeartBeat;
    bool isHeartBeat = false;

    void Start ()
    {
        playerGui = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGui>();
        animator = GetComponent<Animator>();

        asHeartBeat = transform.Find("HeartBeat").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (dead)
        {
            asHeartBeat.Stop();
            isHeartBeat = false;
            poetryBuff = -1;
            if (!deathAnimationStarted)
            {
                animator.SetBool("Dead", true);
                gameObject.layer = 13;
                GetComponent<PlayerSoundHandler>().Die();
                deathAnimationStarted = true;
                deathtime = Time.time;
            }
            else if (deathtime + respawnDelay <= Time.time){
                dead = false;
                gameObject.layer = 9;
                GetComponent<PlayerController>().KnockBack(0);
                animator.SetBool("Dead", false);
                hp = maxHP;
                deathAnimationStarted = false;

                GetComponent<PlayerSpawnHandler>().Respawn();
            }
        }
        else
        {
            if(hp<=0)
            {
                dead = true;
            }
            if(!isHeartBeat && hp<=2)
            {

                asHeartBeat.Play();
                isHeartBeat = true;
            }
            poetry();
            //if (dmgTimer>0)
            //{
            //    dmgTimer -= Time.deltaTime;
            //}
        }
	}

    void poetry()
    {
        //print(poetryBuff);
        if (poetryBuff < -poetryCD)
        {
            if (Input.GetAxis("Poetry") != 0 && playerGui.skillLevel >= 4 && !GetComponent<PlayerController>().dead)
            {
                animator.SetBool("CastingPoetry", true);
                if (!playedPoetrySound)
                {
                    playedPoetrySound = true;
                    Debug.Log("casting poetry");
                    GetComponent<PlayerSoundHandler>().CastPoetry();
                }
                
                poetryCasting += Time.deltaTime;
                if (poetryCasting >= poetryCastTime)
                {
                    poetryBuff = poetryTime;
                    animator.SetBool("CastingPoetry", false);
                    playedPoetrySound = false;
                }
            }
            else
            {
                poetryCasting = 0;
                animator.SetBool("CastingPoetry", false);
                if (playedPoetrySound)
                    GetComponent<AudioSource>().Stop();
                playedPoetrySound = false;
            }
        }

        else
        {
            poetryCasting = 0;
            poetryBuff -= Time.deltaTime;
            //print("poetryBuff:" + poetryBuff);
        }
    }

    //Wenn der Spieler im DMG des Gegners steht und er verwundbar ist, bekommt er Schaden und wird kurzzeitig unverwundbar
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (/*dmgTimer<=0 && */collision.gameObject.tag == "DmgToPlayer")
        {
            //Variables

            EnemyDMG enemyDMG = collision.GetComponent<EnemyDMG>();
            int dir = 1;

            //Prüft ob Gegner bereits erneut Schaden verursachen kann
            if (enemyDMG.timer < 0)
            {
                //DMG to player
                hp -= enemyDMG.dmg;
                if (!dead)
                    GetComponent<PlayerSoundHandler>().Hit();
                //print("HP: " + hp);

                //UnverwundbarkeitsTimer
                //dmgTimer = dmgCD;
                //print("Timer: " + dmgTimer);

                //Knockback
                if (collision.transform.position.x > transform.position.x)
                {
                    dir = -1;
                }
                GetComponent<PlayerController>().KnockBack(enemyDMG.knockback * dir);
                //print("Knockback: " + enemyDMG.knockback);

                //Timer bis Gegner erneut Schaden verursachen kann
                enemyDMG.timer = enemyDMG.dmgTime;
            }
        }
        if (/*dmgTimer <= 0 && */poetryBuff <= 0 && collision.gameObject.tag == "PoetryDmgToPlayer")
        {
            //Variables
            EnemyDMG enemyDMG = collision.GetComponent<EnemyDMG>();

            //Die Sirene verursacht Schaden pro Sekunde!
            hp -= Time.deltaTime * enemyDMG.dmg;
            //print("HP: " + hp);

            //Prüft ob Gegner bereits erneut Schaden verursachen kann
            if (enemyDMG.timer < 0)
            {
                //Die Sirene verursacht Schaden pro Sekunde!
                hp -= Time.deltaTime * enemyDMG.dmg;
                GetComponent<PlayerSoundHandler>().Hit();
                //print("HP: " + hp);

                //UnverwundbarkeitsTimer
                //dmgTimer = dmgCD;
                //print("Timer: " + dmgTimer);

                //Timer bis Gegner erneut Schaden verursachen kann
                enemyDMG.timer = enemyDMG.dmgTime;
            }
        }
    }
}
