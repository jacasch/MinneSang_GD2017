using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    int dir = 1;
    float distPlayer = 0.8f;  //Front
    float hitTimer;
    float hitDuration = 0.2f;
    public float repeatCD = 0.75f;
    float repeatTimer;

    BoxCollider2D collider;
    Animator animator;

    GameObject hitEffect;
    ParticleSystem particleSystem;

    HitSound hitSound;

	// Use this for initialization
	void Start ()
    {
        collider = GetComponent<BoxCollider2D>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        hitEffect = transform.GetChild(0).gameObject;
        particleSystem = hitEffect.GetComponent<ParticleSystem>();
        hitSound = GetComponent<HitSound>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //flipping
        if (!animator.GetBool("Attacking")) {
            if (Input.GetAxis("Horizontal") < 0)
            {
                dir = -1;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                dir = 1;
            }
        }
        transform.localPosition = new Vector3(distPlayer * dir, 0.15f, 0);

        if(hitTimer>=0)
        {
            collider.enabled = true;
            hitTimer -= Time.deltaTime;
            animator.SetBool("Attacking", true);
        }
        else
        {
            collider.enabled = false;
            animator.SetBool("Attacking", false);
        }

        if(Input.GetAxis("Attack") > 0 && repeatTimer<= 0 
            && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().poetryCasting == 0
            && GameObject.FindGameObjectWithTag("StunToEnemy").GetComponent<PlayerStun>().castTimer == 0)  //INPUT!!
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoundHandler>().Attack();
            hitTimer = hitDuration;
            repeatTimer = repeatCD;
        }
        repeatTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            hitEffect.transform.localPosition = new Vector3(8.5f * dir, 15, 0);
            hitSound.Hitting();

            if (collision.GetComponent<EnemyOoze>() != null && !collision.GetComponent<EnemyOoze>().stealth)
            {
                particleSystem.Play();
            }

            if (collision.GetComponent<EnemyDance>() != null && !collision.GetComponent<EnemyDance>().stealth)
            {
                particleSystem.Play();
            }

            if (collision.GetComponent<EnemyPaint>() != null && !collision.GetComponent<EnemyPaint>().stealth)
            {
                particleSystem.Play();
            }

            if (collision.GetComponent<EnemyMusic>() != null && !collision.GetComponent<EnemyMusic>().stealth)
            {
                particleSystem.Play();
            }

            if (collision.GetComponent<EnemyPoetry>() != null && !collision.GetComponent<EnemyPoetry>().stealth)
            {
                particleSystem.Play();
            }
        }
    }


}
