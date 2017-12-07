using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    int dir = 1;
    float distPlayer = 2.5f;  //Front
    float stunTimer;
    float stunDuration = 0.15f;
    float repeatCD = 0f;
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
        if (Input.GetAxis("Horizontal") < 0)
        {
            dir = -1;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            dir = 1;
        }
        transform.localPosition = new Vector3(distPlayer * dir, 0, 0);

        if (stunTimer >= 0)
        {
            collider.enabled = true;
            stunTimer -= Time.deltaTime;
        }
        else
        {
            collider.enabled = false;
        }

        if (Input.GetAxis("Stun") > 0 && repeatTimer <= 0)  //INPUT!!
        {
            stunTimer = stunDuration;
            repeatTimer = repeatCD;
        }
        repeatTimer -= Time.deltaTime;
    }
}
