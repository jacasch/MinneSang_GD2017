using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour {
    public string npcName;
    private string playerName = "Knight";
    private string princess = "Princess Freya";

    public Phrase[] questDialogue;
    public Phrase[] randomLines;
    public bool autoStart = false;


    private float talkDelay = 1.5f;
    private float lastTalk;
    private GameObject textObject; //BUG: nullpointer error after player leaves interactionzone
    private GameObject npcNameObject;
    private Text textBox;
    private Text npcNameBox;

    private RectTransform textTransform;
    private RectTransform npcNameTransform;

    private bool inRange = false;
    private bool interacting = false;

    private float ratio;

    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public Phrase activePhrase;
    [HideInInspector]
    public int activePhraseIndex;


    // Use this for initialization
    void Start () {
        //textObject = npcName.gameObject;
        textObject = transform.Find("Canvas/Text").gameObject;
        textBox = textObject.GetComponent<Text>();
        npcNameObject = transform.Find("Canvas/Text/npcNameObject").gameObject;
        npcNameBox = npcNameObject.GetComponent<Text>();
        npcNameBox.text = npcName;
        textTransform = textObject.GetComponent<RectTransform>();
        textObject.SetActive(false);
        //activePhrase = randomLines[0];
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {


        if (inRange) {
            Talk(activePhrase);
            //check for interaction input
            if (Input.GetButton("Jump") && !player.GetComponent<PlayerController>().dead && player.GetComponent<PlayerController>().grounded)
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
        autoStartConvo();
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

    protected virtual void Talk(Phrase phrase) {
        
        lastTalk += Time.deltaTime;

        textBox.text = phrase.text;
        GameObject worldObject = phrase.spokenByNpc || phrase.messageByPrincess ? gameObject : player;

        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(worldObject.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        (viewportPosition.x * Screen.width),
        (viewportPosition.y * Screen.height));
        if (phrase.spokenByNpc)
        {
            npcNameBox.text = npcName.ToUpper();
        }
        else if (phrase.messageByPrincess)
        {
            npcNameBox.text = princess.ToUpper();
        }
        else
        {
            npcNameBox.text = playerName.ToUpper();
        }
        /* float ratioWidth = Screen.width;
         float ratioHeight = Screen.height;
         float ratio = ratioHeight / ratioWidth;
         Debug.Log(ratio);
         textBox.fontSize = Mathf.RoundToInt(ratio*30); */

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

    private void autoStartConvo() {
        if (autoStart && inRange && player.GetComponent<PlayerQuestHandler>().activeQuest == "mastered")
        {
            if (!interacting)
            {
                StartInteraction();
                Talk(activePhrase);
                Interact();
            }
        }
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

    public virtual void Initialize() {
    }

    public virtual void NextPhrase() {
    }

    public void EndInteraction() {
        inRange = false;
        interacting = false;
        Camera.main.GetComponent<CameraMovement>().ZoomOut();
        textObject.SetActive(false);
    }
}
