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
        bc2d.isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            playerIsInsidePlatform = true;
        }
        if ((collision.gameObject.tag == "Paint" || collision.gameObject.name == "EnemyPaintShot(Clone)") && !playerIsInsidePlatform) {
            bc2d.isTrigger = false;
            if (collision.gameObject.tag == "Paint")
                collision.gameObject.GetComponent<PaintShot>().Explode();
            sr.material = defaultmat;
            bc2d.isTrigger = false;
            sr.sortingLayerName = "MidGround";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsInsidePlatform = false;
            print("player left");
        }
    }
}
