using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour {
    public string name;
    public string destinationScene;
    public string destinationDoor;
    float soundTimer = 0;
    float enterTimer = 0;
    bool isSound = false;
    bool enter = false;

    DoorSounds doorSound;
    
    private GameObject player;
    GameObject panel;

    private bool playerInRange = false;

	// Use this for initialization
	void Start () {
        doorSound = GetComponent<DoorSounds>();
    }

    // Update is called once per frame
    void Update () {
        GameObject panel = GameObject.Find("TOP/Fade");

        if (playerInRange)
            if (player.GetComponent<PlayerController>().grounded)
                CheckInput();
        if(enter)
        {
            enterTimer += Time.deltaTime;
            if(enterTimer >= 1)
            {
                enter = false;
                isSound = false;
                enterTimer = 0;
                player.GetComponent<PlayerSpawnHandler>().switched = true;

                ChangeScene();
            }
        }
	}

    private void CheckInput() {
        if (Input.GetAxis("Vertical") >= 0.9f)
        {
            if (Input.GetAxis("Horizontal") > -0.3f && Input.GetAxis("Horizontal") < 0.3f)
            {
                if (soundTimer >=0.25f)
                {
                    print(enter);
                    enter = true;
                    if (!isSound)
                    {
                        doorSound.Open();
                    }
                    isSound = true;
                }
                else
                {
                    soundTimer += Time.deltaTime;
                }
            }
            else
            {
                soundTimer = 0;
                isSound = false;
            }
        }
        else
        {
            soundTimer = 0;
            isSound = false;
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
