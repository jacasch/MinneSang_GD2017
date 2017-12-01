using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaintShot : MonoBehaviour {
    public GameObject prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Paint")) {
            GameObject instance = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
            instance.GetComponent<PaintShot>().direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") + 0.1f, 0);
        }
	}
}
