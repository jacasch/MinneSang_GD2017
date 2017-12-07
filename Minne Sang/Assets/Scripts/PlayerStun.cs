using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    int dir = 1;
    float distPlayer = 2.25f;  //Front
    float stunTimer;
    float castDuration = 0.15f;
    public float castTime = 1f;
    float castTimer = 0;
    public float repeatCD = 5f;
    float repeatTimer = 0;

    BoxCollider2D boxCollider;

    // Use this for initialization
    void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
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
        boxCollider.offset = new Vector2(0f * dir,0);

        if (repeatTimer < 0)
        {
            if (Input.GetAxis("Stun") != 0)
            {
                castTimer += Time.deltaTime;
                if(castTimer >= castTime)
                {
                    stunTimer = castDuration;
                    repeatTimer = repeatCD;
                    boxCollider.enabled = true;
                    castTimer = 0;
                }
            }
            else
            {
                castTimer = 0;
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
                repeatTimer -= Time.deltaTime;
                boxCollider.enabled = false;
            }
        }










    }
}
