using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{

    SpriteRenderer mySprite;
    AudioSource audioSource;
    BoxCollider2D boxCol;

    float destroyTimer = 0.5f;

    bool destroy = false;

	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        mySprite = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(destroy)
        {
            destroyTimer -= Time.deltaTime;
            if(destroyTimer<=0)
            {
                Destroy(gameObject);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "StunToEnemy")
        {
            destroy = true;
            //ParticleEffects?
            audioSource.Play();
            mySprite.enabled = false;
            boxCol.enabled = false;
        }
    }
}
