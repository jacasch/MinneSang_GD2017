using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonPlatform : MonoBehaviour {
    SpriteRenderer sr;
    BoxCollider2D bc2d;
    public Material defaultmat;
    public Material chameleonmat;

    private bool playerIsInsidePlatform = false;


	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        sr.material = chameleonmat;
        bc2d.isTrigger = false;
        gameObject.layer = 8;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            playerIsInsidePlatform = true;
        }
        if (collision.gameObject.tag == "Paint" && !playerIsInsidePlatform) {
            bc2d.isTrigger = false;
            Destroy(collision.gameObject);
            sr.material = defaultmat;
            gameObject.layer = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsInsidePlatform = false;
        }
    }
}
