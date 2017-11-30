using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour {
    [Range(-3,3)]
    [Tooltip("Lower is closer to Camera")]
    public float depth = 0f;
    [Tooltip("The player that is moving in the scene")]
    public GameObject camera;

    protected float scaleFactor;

	// Use this for initialization
	void Start () {
        scaleFactor = Mathf.Pow(((depth * -1 + 3) / 3), 2);
        Debug.Log(depth + " " + scaleFactor);
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, depth);
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y, 0);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, depth);
    }
	
	// Update is called once per frame
	void Update () {        
        transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, depth) * scaleFactor * (depth / Mathf.Abs(depth));
        transform.position = new Vector3(transform.position.x, transform.position.y, depth);
    }
}
