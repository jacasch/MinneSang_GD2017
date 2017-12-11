using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaintShot : MonoBehaviour {
    public GameObject prefab;
    private PlayerGui pg;
    private PlayerController pc;
    public bool aiming = false;

	// Use this for initialization
	void Start () {
        pg = GetComponent<PlayerGui>();
        pc = GetComponent<PlayerController>();
        pc.aiming = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Paint") && pg.skillLevel >= 2) {
            pc.aiming = true;
        }
        
        if (Input.GetButtonUp("Paint") && pg.skillLevel >= 2) {
            GameObject instance = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
            instance.GetComponent<PaintShot>().direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") + 0.1f, 0);
            pc.aiming = false;
        }
	}
}
