using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestHandler : MonoBehaviour {
    public string activeQuest;
    public int activeAct;
    public int letters = 0;
    public bool letterSend = false;

    private int delay = 100;
    private bool menuEnabled = false;
//    public bool optionToSendLetters = false;
    public List<string> missingItems = new List<string>();
    public List<string> collectedItems = new List<string>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        menuHandler();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent != null)
        {
            if (collision.gameObject.transform.parent.gameObject.tag == "Item")
            {
                AddItem(collision.gameObject.transform.parent.gameObject.GetComponent<ItemHandler>().GetName());
                print("picked up");
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
        }
    }

    public void menuHandler()
    {
        GameObject tempObject = GameObject.Find("menu");
        Canvas canvasMenu = tempObject.GetComponent<Canvas>();

        if (menuEnabled == false)
        {
            canvasMenu.enabled = false;
        }

        if (Input.GetKeyDown("joystick 1 button 7"))
        {
            if (menuEnabled == false)
            {
                canvasMenu.enabled = true;
                Time.timeScale = 1.0f;
                canvasMenu.enabled = true;
                menuEnabled = true;
                Debug.Log("Start open", gameObject);
                menuTextQuest();
        }
        else
            {
                Time.timeScale = 1.0f;
                canvasMenu.enabled = false;
                menuEnabled = false;
                Debug.Log("Start closed", gameObject);
            }
        }
    }

    public void AddItem(string item) {
        if (!collectedItems.Contains(item)) {
            collectedItems.Add(item);
        }      
    }

    public bool HasItem(string item) {
        return !collectedItems.Contains(item);
    }

    public void menuTextQuest()
    {
        GameObject tempText = transform.Find("menu/QuestText").gameObject;
        Text questText = tempText.GetComponent<Text>();
        GameObject player = GameObject.Find("player");

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "mastered") {
        questText.text = "Go speak to the Headmaster";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "running" && player.GetComponent<PlayerQuestHandler>().activeAct == 0)
        {
            questText.text = "You've met the headmaster, who sent you off to meet the high masters. Onwards, to master the arts and finally true love!";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q1")
        {
            GameObject master = GameObject.Find("master_of_dance");
            int itemLength = master.GetComponent<MasterNpc>().quest.itemsToCollect.Length;
            int font_size = 22;

            for (int i = 0; i < itemLength; i++)
            {
                GameObject itemsQuest = new GameObject("Item"+i);
                itemsQuest.transform.SetParent(transform.Find("menu"));

                Text text = itemsQuest.AddComponent<Text>();
                text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                text.text = master.GetComponent<MasterNpc>().quest.itemsToCollect[i];
                text.fontSize = 18;
                text.color = Color.white;
                text.alignment = TextAnchor.MiddleCenter;
                text.transform.position = new Vector2(Screen.width/2, Screen.height/2 - 50 - i * 25);
            }

            questText.text = "The high master of dance lost all his clothes. Bring him his tiara, his tutu and shoes and maybe he will instruct you in the art of dance. ";
        }
    }
}
