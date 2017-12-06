using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour {
    private Door[] doors;

    // Use this for initialization    
	void Start () {
        doors = FindObjectsOfType(typeof(Door)) as Door[];
        foreach (Door d in doors) {
            //print(d.gameObject.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
