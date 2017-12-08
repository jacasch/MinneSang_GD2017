using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnPoint : MonoBehaviour {
    public string name;

    private SpawnPoint[] otherSpawnPoints;
    private BoxCollider2D collider;
    // Use this for initialization
    void Awake()
    {
        otherSpawnPoints = FindObjectsOfType(typeof(SpawnPoint)) as SpawnPoint[];
        for (int i = 0; i < otherSpawnPoints.Length; i++) {
            int xPos = Mathf.RoundToInt(otherSpawnPoints[i].transform.position.x);
            int yPos = Mathf.RoundToInt(otherSpawnPoints[i].transform.position.y);
            otherSpawnPoints[i].name = "SpawnPoint" + xPos.ToString() + ";" + yPos.ToString();
        }

        collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.gameObject.GetComponent<PlayerSpawnHandler>().targetSpawn = name;
            print(name);
            collision.gameObject.GetComponent<PlayerSpawnHandler>().targetScene = SceneManager.GetActiveScene().name;
        }
    }
}
