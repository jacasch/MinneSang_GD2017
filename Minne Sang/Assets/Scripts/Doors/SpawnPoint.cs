using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnPoint : MonoBehaviour {
    public string name;
    public bool isMasteryStart = false;

    private SpawnPoint[] otherSpawnPoints;
    private BoxCollider2D collider;

    private PlayerQuestHandler pqh;
    private PlayerSpawnHandler psh;
    // Use this for initialization
    void Awake()
    {
        if (!isMasteryStart)
        {
            int xPos = Mathf.RoundToInt(transform.position.x);
            int yPos = Mathf.RoundToInt(transform.position.y);
            name = "SpawnPoint" + xPos.ToString() + ";" + yPos.ToString();
        }
        else {
            name = "masterystart";
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
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerSpawnHandler>().targetSpawn = name;
            collision.gameObject.GetComponent<PlayerSpawnHandler>().targetScene = SceneManager.GetActiveScene().name;

            collision.gameObject.GetComponent<PlayerSpawnHandler>().SaveGameState();
        }
    }
}
