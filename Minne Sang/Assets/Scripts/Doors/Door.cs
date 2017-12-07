using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
    public string name;
    public string destinationScene;
    public string destinationDoor;
    
    private GameObject player;

    private bool playerInRange = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInRange)
            if (player.GetComponent<PlayerController>().grounded)
                CheckInput();
		
	}

    private void CheckInput() {
        if (Input.GetAxis("Vertical") >= 0.9f)
        {
            if (Input.GetAxis("Horizontal") > -0.3f && Input.GetAxis("Horizontal") < 0.3f)
            {
                player.GetComponent<PlayerSpawnHandler>().switched = true;
                ChangeScene();
            }
        }
    }

    private void ChangeScene() {
        player.GetComponent<PlayerSpawnHandler>().targetSpawn = destinationDoor;
        player.GetComponent<PlayerSpawnHandler>().targetScene = destinationScene;
        player.GetComponent<PlayerSpawnHandler>().Respawn();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
