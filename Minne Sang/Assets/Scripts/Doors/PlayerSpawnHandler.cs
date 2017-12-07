using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnHandler : MonoBehaviour {
    public string targetSpawn;
    public GameObject playerActions;
    [HideInInspector]
    public bool switched = false;
    private Door[] doors;

    private void Start()
    {
        Respawn();
        //if there is another player in the scen destroy it
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (p != gameObject)
            {
                //Destroy(gameObject);
                //Camera.main.GetComponent<CameraMovement>().player = p;
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        //print(Camera.main.gameObject.name);
        //Camera.main.GetComponent<CameraMovement>().player = gameObject;
        Respawn();
    }

    public void Respawn() {

        doors = FindObjectsOfType(typeof(Door)) as Door[];
        foreach (Door d in doors)
        {
            if (d.name == targetSpawn) {
                transform.position = d.transform.position;
                Camera.main.transform.position = new Vector3(d.transform.position.x, d.transform.position.y, -10);
            }
        }
    }
}
