﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonPlatform : MonoBehaviour {
    SpriteRenderer sr;
    BoxCollider2D bc2d;
    public Material defaultmat;
    public Material chameleonmat;


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
        if (collision.gameObject.tag == "Paint") {
            bc2d.isTrigger = false;
            Destroy(collision.gameObject);
            sr.material = defaultmat;
            gameObject.layer = 0;
        }
    }
}