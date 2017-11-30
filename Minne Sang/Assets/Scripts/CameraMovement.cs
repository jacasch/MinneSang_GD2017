using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float maxPlayerDistance = 10f;
    [Range(0f,1f)]
    public float lerpWeight = 0.9f;
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        float distance = (pos - playerPos).magnitude;

        if (distance > maxPlayerDistance) {
            //move camera towards player by amount bigger than max dist
            Vector2 movement = (pos - playerPos).normalized * (maxPlayerDistance - distance);
            Vector3 movement3D = new Vector3(movement.x, movement.y, 0f);
            transform.position = Vector3.Lerp(transform.position + movement3D, transform.position, lerpWeight);

        }
	}
}
