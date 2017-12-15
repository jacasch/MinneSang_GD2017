using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPaintShot : MonoBehaviour
{
    float liveTime = 5;
    float speed = 7;

    public Vector3 direction;
    public float velocity;
    public AudioClip explodeSound;
    public Color color;
    Rigidbody2D rb2d;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start ()
    {
        gameObject.layer = 0;
        direction.z = 0;
        direction = direction.normalized;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = direction * velocity;
    }

    void Update()
    {
        liveTime -= Time.deltaTime;
        if(liveTime<=0)
        {
            Explode();
        }
    }

    //FUNCTIONS------------------------------------------------------------------------------------------------------------
    //Prüfung der Collision
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag != "Enemy" && other.tag != "DmgToPlayer" && other.tag != "Detection")
        {
            Explode();
        }
    }

    public void Explode()
    {
        GameObject.FindGameObjectWithTag("PaintSplatter").GetComponent<PaintSplatter>().Paint(new Vector2(transform.position.x, transform.position.y), color);
        GetComponent<AudioSource>().clip = explodeSound;
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, explodeSound.length);
    }
}
