using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {
    public string relatedQuest;
    public Phrase[] questDialogue;    
    public Item questDrop;
    public Phrase[] randomLines;
    public float talkDelay = 3f;

    private bool inRange = false;
    private bool interacting = false;
    private GameObject player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (inRange) {
            //check for interaction input
            if (Input.GetButtonDown("Jump"))
            {
                interacting = true;
                if (player.GetComponent<PlayerQuestHandler>().activeQuest == relatedQuest)
                {
                    //start dialogue                    
                    Debug.Log("QuestDialogue");
                    //drop item
                }
                else
                {
                    //random line
                    Debug.Log("RandomLine");
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
            player = collision.gameObject;
            Camera.main.GetComponent<CameraMovement>().ZoomIn(new Vector2(transform.position.x, transform.position.y));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;
            Camera.main.GetComponent<CameraMovement>().ZoomOut();
        }
    }

    private void Talk(Phrase phrase,bool self) {
        //
    }
}
