using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    int dir = 1;
    float distPlayer = 1;  //Front
    float hitTimer;
    float hitDuration = 0.15f;
    public float repeatCD = 0.75f;
    float repeatTimer;

    BoxCollider2D collider;

	// Use this for initialization
	void Start ()
    {
        collider = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetAxis("Horizontal") < 0)
        {
            dir = -1;
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            dir = 1;
        }
        transform.localPosition = new Vector3(distPlayer * dir, 0, 0);

        if(hitTimer>=0)
        {
            collider.enabled = true;
            hitTimer -= Time.deltaTime;
        }
        else
        {
            collider.enabled = false;
        }

        if(Input.GetAxis("Attack") > 0 && repeatTimer<=0)  //INPUT!!
        {
            hitTimer = hitDuration;
            repeatTimer = repeatCD;
        }
        repeatTimer -= Time.deltaTime;
    }




}
