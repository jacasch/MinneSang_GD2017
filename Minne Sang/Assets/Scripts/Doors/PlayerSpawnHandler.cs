using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnHandler : MonoBehaviour {
    public string targetSpawn;
    public string targetScene;
    public GameObject playerActions;
    [HideInInspector]
    public bool switched = false;
    private Door[] doors;
    private SpawnPoint[] spawnPoints;


    private void Start()
    {
        Reposition();
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
        Reposition();
    }

    private void Reposition() {
        doors = FindObjectsOfType(typeof(Door)) as Door[];
        spawnPoints = FindObjectsOfType(typeof(SpawnPoint)) as SpawnPoint[];
        foreach (Door d in doors)
        {
            if (d.name == targetSpawn)
            {
                transform.position = d.transform.position;
                Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            }
        }
        foreach (SpawnPoint p in spawnPoints) {
            if (p.name == targetSpawn) {
                transform.position = p.transform.position;
                Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            }
        }
    }

    public void Respawn() {
        SceneManager.LoadScene(targetScene,LoadSceneMode.Single);
    }
}
