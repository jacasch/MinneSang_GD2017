using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPaintShot : MonoBehaviour
{
    float liveTime = 5;
    float speed = 5;

	// Use this for initialization
	void Start ()
    {
		
	}

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.forward*speed * Time.deltaTime);
        //transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
