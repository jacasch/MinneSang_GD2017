using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintShot : MonoBehaviour {

    public Vector3 direction;
    public float velocity;

    public AudioClip[] explodeSound;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.layer != 1)
        {
            Explode();
        }
    }

    public void Explode() {
        GameObject.FindGameObjectWithTag("PaintSplatter").GetComponent<PaintSplatter>().Paint(new Vector2(transform.position.x, transform.position.y));
        GetComponent<AudioSource>().clip = explodeSound[0];
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, explodeSound.Length);
    }
}
