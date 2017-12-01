using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintShot : MonoBehaviour {

    public Vector3 direction;
    public float velocity;
    Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        direction.z = 0;
        direction = direction.normalized;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = direction * velocity;
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
