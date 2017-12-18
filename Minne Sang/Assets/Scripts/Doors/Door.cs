using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour {
    public string name;
    public string destinationScene;
    public string destinationDoor;
    
    private GameObject player;
    GameObject panel;

    private bool playerInRange = false;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        GameObject panel = GameObject.Find("TOP/Fade");

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
                GameObject panel = GameObject.Find("Fade");
                panel.GetComponent<fade>().fadeIn();
                ChangeScene();
            }
        }
    }

    private void ChangeScene() {
        GameObject panel = GameObject.Find("Fade");

        if (panel.GetComponent<fade>().fadeIn())
        {
            player.GetComponent<PlayerSpawnHandler>().targetSpawn = destinationDoor;
            player.GetComponent<PlayerSpawnHandler>().targetScene = destinationScene;
            player.GetComponent<PlayerSpawnHandler>().Respawn();
        }  
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
