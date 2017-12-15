using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaintShot : MonoBehaviour {
    public GameObject prefab;
    private PlayerGui pg;
    private PlayerController pc;
    private SpriteRenderer sr;
    private LineRenderer lr;
    private Animator animator;
    public bool aiming = false;

	// Use this for initialization
	void Start () {
        pg = GetComponent<PlayerGui>();
        pc = GetComponent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        lr.enabled = false;
        lr.sortingLayerName = "Player";
        lr.sortingOrder = -100;
        pc.aiming = false;
	}
	
	// Update is called once per frame
	void Update () {
        bool canPaint = pg.skillLevel >= 2 && !animator.GetBool("CastingPoetry") && !animator.GetBool("CastingMusic") && !pc.dead;

        if (canPaint)
        {

            if (Input.GetButtonDown("Paint"))
            {
                pc.aiming = true;
                lr.enabled = true;
                DrawArc();
            }
            if (Input.GetButton("Paint"))
            {
                DrawArc();

            }

            if (Input.GetButtonUp("Paint"))
            {
                GameObject instance = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
                Vector3 direction = (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) ? new Vector3(sr.flipX ? -1 : 1, 1, 0) : new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") + 0.1f, 0);
                instance.GetComponent<PaintShot>().direction = direction;
                pc.aiming = false;
                lr.enabled = false;
            }
        }
        else {
            pc.aiming = false;
            lr.enabled = false;
        }
	}

    void DrawArc() {
        Vector3 direction = ((Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) ? new Vector3(sr.flipX ? -1 : 1, 1, 0) : new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") + 0.1f, 0)).normalized;
        lr.positionCount = 50;
        Vector3 pos = transform.position;
        Vector3 lastPos = pos;
        lr.SetPosition(0, pos);
        Vector3 yVelocity = new Vector3(0,0,0);
        Vector3 gravity = new Vector3(0, -0.025f, 0);
        float timeStep = 0.2f;

        for (int step = 1; step < 50; step++) {
            pos = lastPos + direction * timeStep + yVelocity;
            lastPos = pos;
            yVelocity += gravity * timeStep;
            lr.SetPosition(step, pos);
        }
    }
}
