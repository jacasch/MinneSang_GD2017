using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour {
    public string relatedQuest;
    public Phrase[] questDialogue;
    public Phrase afterDropLine;
    public Item questDrop;
    public Phrase[] randomLines;
    public float talkDelay = 3f;

    private float lastTalk;
    private GameObject textObject;
    private Text textBox;
    private RectTransform textTransform;
    private bool inRange = false;
    private bool interacting = false;
    private GameObject player;

    private Phrase activePhrase;
    private int activePhraseIndex;


    // Use this for initialization
    void Start () {
        textObject = GetComponentInChildren<Text>().gameObject;
        textBox = textObject.GetComponent<Text>();
        textTransform = textObject.GetComponent<RectTransform>();
        textObject.SetActive(false);
        activePhrase = questDialogue[0];
	}
	
	// Update is called once per frame
	void Update () {
        if (inRange) {
            Talk(questDialogue[0]);
            //check for interaction input
            if (Input.GetButton("Jump"))
            {
                if (!interacting)
                {
                    StartInteraction();
                }
            }
        }

        if (interacting) {
            Interact();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EndInteraction();
        }
    }

    private void Talk(Phrase phrase) {
        lastTalk += Time.deltaTime;
        textBox.text = phrase.text;
        GameObject worldObject = phrase.spokenByNpc ? gameObject : player;

        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldObject.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        (viewportPosition.x * Screen.width),
        (viewportPosition.y * Screen.height));
        /*Vector2 WorldObject_ScreenPosition = new Vector2(
        ((viewportPosition.x * textTransform.sizeDelta.x) - (textTransform.sizeDelta.x * 0.5f)),
        ((viewportPosition.y * textTransform.sizeDelta.y) - (textTransform.sizeDelta.y * 0.5f)));*/

        textTransform.anchoredPosition = WorldObject_ScreenPosition + new Vector2(0, Screen.height/3);
    }

    private void StartInteraction() {
        activePhraseIndex = 0;
        NextPhrase();   
        interacting = true;
        Camera.main.GetComponent<CameraMovement>().ZoomIn(transform.position);
        textObject.SetActive(true);
        lastTalk = 0;
    }

    private void Interact() {
        Talk(activePhrase);
        //check if we are ready to check for the next phrase
        if (lastTalk > talkDelay) {
            //check for input
            if (Input.GetButtonDown("Jump")) {
                //change phrase depending on stuff ^^
                NextPhrase();
            }
        }
    }

    private void NextPhrase() {
        Debug.Log("nexte phrase");
        //if we are in the right quest
        if (relatedQuest == player.GetComponent<PlayerQuestHandler>().activeQuest)
        {
            //check if player already has the item we would give him
            if (!player.GetComponent<PlayerQuestHandler>().HasItem(questDrop.name)) {
                activePhrase = afterDropLine;
            }else
            //check if quest dialogue hes ended
            if (activePhraseIndex >= questDialogue.Length) {
                Debug.Log("too long");
                //drop the questdrop
                DropItem();
                //abort conversation
                EndInteraction();
            }// else we are still talking in the dialogue
            else {
                activePhrase = questDialogue[activePhraseIndex];
                activePhraseIndex++;
                Debug.Log(activePhraseIndex);
            }
        }
        //if we are not in the right quest to dialoughe with this npc
        else
        {
            int newIndex;
            do {
                newIndex = Random.Range(0, randomLines.Length);
            } while (newIndex == activePhraseIndex);

            activePhraseIndex = newIndex;
            activePhrase = randomLines[activePhraseIndex];
        }
    }

    private void EndInteraction() {
        inRange = false;
        interacting = false;
        Camera.main.GetComponent<CameraMovement>().ZoomOut();
        textObject.SetActive(false);
    }

    private void DropItem() {
        GameObject drop = Instantiate(questDrop.drop, transform.position, transform.rotation);
        drop.GetComponent<ItemHandler>().SetName(questDrop.name);
    }
}
