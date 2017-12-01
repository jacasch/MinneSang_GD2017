using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPaintShot : MonoBehaviour
{
    float liveTime = 5;
    float speed = 5;

    public Vector3 direction;
    public float velocity;
    Rigidbody2D rb2d;

    // Use this for initialization
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
            Destroy(gameObject);
        }
        Move();
    }

    void Move()
    {
        //transform.Translate(Vector3.right*speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag != "Enemy" && other.tag != "DmgToPlayer" && other.tag != "Detection")
        {
            Destroy(gameObject);
        }
    }
}
