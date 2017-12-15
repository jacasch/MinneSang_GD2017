using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintShot : MonoBehaviour {

    public Vector3 direction;
    public float velocity;

    public AudioClip[] explodeSound;
    Rigidbody2D rb2d;

    public Color[] paintColors;
    int colorIndex;

    // Use this for initialization
    void Start () {
        colorIndex = (int)Random.Range(0, paintColors.Length);
        GetComponent<SpriteRenderer>().color = paintColors[colorIndex];
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
        GameObject.FindGameObjectWithTag("PaintSplatter").GetComponent<PaintSplatter>().Paint(new Vector2(transform.position.x, transform.position.y), paintColors[colorIndex]);
        GetComponent<AudioSource>().clip = explodeSound[0];
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, explodeSound[0].length);
    }
}
