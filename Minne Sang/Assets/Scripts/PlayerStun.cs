using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    int dir = 1;
    float distPlayer = 2.25f;  //Front
    public float stunTimer;
    float castDuration = 0.15f;
    public float castTime = 0.75f;
    [HideInInspector]
    public float castTimer = 0;
    public float repeatCD = 0f;
    float repeatTimer = 0;
    float particleDelay = 0.5f;
    float particleStartTime;

    private bool soundPlayed = false;
    private float soundTimer = 0.75f;

    private GameObject particles;

    BoxCollider2D boxCollider;
    Animator animator;
    UnityEngine.ParticleSystem.ShapeModule shapeModule;

    PlayerGui playerGui;

    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        playerGui = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGui>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        particles = transform.Find("stunParticles").gameObject;
        particles.active = false;

        shapeModule = particles.GetComponent<ParticleSystem>().shape;
    }

    // Update is called once per frame
    void Update()
    {
        if (repeatTimer < 0)
        {
            if (Input.GetAxis("Stun") != 0 && playerGui.skillLevel >= 3 && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().dead)
            {
                animator.SetBool("CastingMusic", true);
                castTimer += Time.deltaTime;
                if (castTimer >= soundTimer && !soundPlayed) {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoundHandler>().Stun();
                    soundPlayed = true;
                    particles.SetActive(true);
                    particleStartTime = Time.time;
                }

                if (castTimer >= castTime)
                {
                    //animator.SetBool("CastingMusic", false);
                    soundPlayed = false;
                    stunTimer = castDuration;
                    repeatTimer = repeatCD;
                    boxCollider.enabled = true;
                    castTimer = 0;
                }
            }
            else
            {
                castTimer = 0;
                animator.SetBool("CastingMusic", false);
                soundPlayed = false;

                if (Input.GetAxis("Horizontal") < 0)
                {
                    dir = -1;
                    shapeModule.rotation = new Vector3(0,0,-68+180);
                }
                else if (Input.GetAxis("Horizontal") > 0)
                {
                    dir = 1;
                    shapeModule.rotation = new Vector3(0, 0, -68);
                }
                transform.localPosition = new Vector3(distPlayer * dir, 0, 0);
                particles.transform.localPosition = new Vector3(-(0.3f * dir), 0.25f, 0);
                boxCollider.offset = new Vector2(0f * dir, 0);
            }
        }
        else
        {
            if (stunTimer > 0)
            {
                stunTimer -= Time.deltaTime;
            }
            else
            {
                boxCollider.enabled = false;
                repeatTimer -= Time.deltaTime;

                if (Input.GetAxis("Horizontal") < 0)
                {
                    dir = -1;
                    shapeModule.rotation = new Vector3(0, 0, -68 + 180);
                }
                else if (Input.GetAxis("Horizontal") > 0)
                {
                    dir = 1;
                    shapeModule.rotation = new Vector3(0, 0, -68);
                }
                transform.localPosition = new Vector3(distPlayer * dir, 0, 0);
                particles.transform.localPosition = new Vector3(-(0.3f * dir), 0.25f, 0);
                boxCollider.offset = new Vector2(0f * dir, 0);
            }
        }

        if (particleStartTime + particleDelay <= Time.time) {
            particles.SetActive(false);
            if (soundPlayed)
            animator.SetBool("CastingMusic", false);
        }
    }
}
