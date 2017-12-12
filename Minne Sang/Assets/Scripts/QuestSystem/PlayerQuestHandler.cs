﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestHandler : MonoBehaviour
{
    public string activeQuest;
    public int activeAct;
    public int letters = 0;
    public bool letterSend = false;

    private int delay = 100;
    private bool menuEnabled = false;
    //    public bool optionToSendLetters = false;
    public List<string> missingItems = new List<string>();
    public List<string> collectedItems = new List<string>();
    public List<string> questItems = new List<string>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKeyDown("joystick 1 button 7") || Input.GetKeyDown("m"))
        {
            if (menuEnabled == false)
            {
                canvasMenu.enabled = true;
                Time.timeScale = 1.0f;
                canvasMenu.enabled = true;
                menuEnabled = true;
                Debug.Log("Start open", gameObject);
                Debug.Log(questItems, gameObject);
                menuTextQuest();
            }
            else
            {
                Time.timeScale = 1.0f;
                canvasMenu.enabled = false;
                menuEnabled = false;
                Debug.Log("Start closed", gameObject);
                menuTextQuestDestroy();
            }
        }
    }


    public void AddItem(string item) {
        if (!collectedItems.Contains(item)) {
            collectedItems.Add(item);
        }
    }

    public bool HasItem(string item)
    {
        return !collectedItems.Contains(item);
    }

    public void menuTextQuest()
    {
        GameObject tempText = transform.Find("menu/QuestText").gameObject;
        Text questText = tempText.GetComponent<Text>();
        GameObject player = GameObject.Find("player");
        Camera cam = Camera.main;

        // After Mastery
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "mastered")
        {
            questText.text = "To master the high arts go and speak to the Headmaster";
        }

        // After End
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "end")
        {
            questText.text = "You mastered all the arts. They even call you the polymath. But you believe you lack something. So you set out to find the perfect gift for the perfect woman.";
        }

        // After talking to Headmaster in different Acts
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "running" && player.GetComponent<PlayerQuestHandler>().activeAct == 0)
        {
            questText.text = "You've met the headmaster, who sent you off to meet the high masters. Onwards, to master the arts and finally true love!";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "running" && player.GetComponent<PlayerQuestHandler>().activeAct == 1)
        {
            questText.text = "You've mastered the art of dance. Next up: Fine arts!";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "running" && player.GetComponent<PlayerQuestHandler>().activeAct == 2)
        {
            questText.text = "You've mastered fine arts and dance. You're next challenge will be the art of music!";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "running" && player.GetComponent<PlayerQuestHandler>().activeAct == 1)
        {
            questText.text = "Three high arts mastered, one left to go. It's time to ask yourself, if poetry really is the art of seduction?";
        }

        // During different Quests
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q1")
        {
            // GameObject master = GameObject.Find("master_of_dance");
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;
            int font_size = 22;

            for (int i = 0; i < itemLength; i++)
            {
                GameObject itemsQuest = new GameObject("Item" + i);
                itemsQuest.transform.SetParent(transform.Find("menu"));

                Text text = itemsQuest.AddComponent<Text>();
                text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                text.text = questItems[i]; //master.GetComponent<MasterNpc>().quest.itemsToCollect[i];
                text.fontSize = 18;
                text.alignment = TextAnchor.LowerCenter;
                text.transform.position = new Vector2(cam.pixelWidth / 2, (Screen.height / 2 - Screen.height / 7) + i * 25);

                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    text.color = Color.red;
                }
                else
                {
                    text.color = Color.white;
                }
            }
            questText.text = "The high master of dance lost all his clothes. Bring him his tiara, his tutu and shoes and maybe he will instruct you in the art of dance. He told you to ask one his students or look around the mainhall.";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q2")
        {
            //GameObject master = GameObject.Find("master_fine_arts");
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;
            int font_size = 22;

            for (int i = 0; i < itemLength; i++)
            {
                GameObject itemsQuest = new GameObject("Item" + i);
                itemsQuest.transform.SetParent(transform.Find("menu"));

                Text text = itemsQuest.AddComponent<Text>();
                text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                text.text = questItems[i];
                text.fontSize = 18;
                text.alignment = TextAnchor.LowerCenter;
                text.transform.position = new Vector2(cam.pixelWidth / 2, (Screen.height / 2 - Screen.height / 7) + i * 25);

                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    text.color = Color.red;
                }
                else
                {
                    text.color = Color.white;
                }
            }
            questText.text = "The fine arts master is in need of a new brush. Craft him one from the finest horse hair and a splendid piece of wood!";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q3")
        {
            //GameObject master = GameObject.Find("master_music");
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;
            int font_size = 22;
            int numberOfItems = 0;

            GameObject itemsQuest = new GameObject("Items");
            itemsQuest.transform.SetParent(transform.Find("menu"));

            for (int i = 0; i < itemLength; i++)
            {
                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    numberOfItems += 1;
                }
            }


            Text text = itemsQuest.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = "Music Sheets: " + numberOfItems + "/" + itemLength;
            text.fontSize = 18;
            text.alignment = TextAnchor.LowerCenter;
            text.transform.position = new Vector2(cam.pixelWidth / 2, Screen.height / 2 - Screen.height / 5);
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(cam.pixelWidth / 2, 30);


            if (numberOfItems == itemLength)
            {
                text.color = Color.red;
            }
            else
            {
                text.color = Color.white;
            }

            questText.text = "The high master of music lost his music sheets to the devious dancers and one of his absent minded students. Retrieve them!";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q4")
        {
            GameObject master = GameObject.Find("master_poetry");
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;
            int font_size = 22;
            int numberOfItems = 0;

            GameObject itemsQuest = new GameObject("Items");
            itemsQuest.transform.SetParent(transform.Find("menu"));

            for (int i = 0; i < itemLength; i++)
            {
                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    numberOfItems += 1;
                }
            }


            Text text = itemsQuest.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = "Ink collected: " + numberOfItems + "/" + itemLength;
            text.fontSize = 18;
            text.alignment = TextAnchor.LowerCenter;
            text.transform.position = new Vector2(cam.pixelWidth / 2, Screen.height / 2 - Screen.height / 5);
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(cam.pixelWidth / 2, 30);


            if (numberOfItems == itemLength)
            {
                text.color = Color.red;
            }
            else
            {
                text.color = Color.white;
            }

            questText.text = "The high master of poetry needs ink to finish his poem. Only then will he teach you. Milk some squids an bring him the liquid of his desire!";
        }
    }


    // Redraw Menus on Input
    public void menuTextQuestDestroy()
    {
        GameObject tempText = transform.Find("menu/QuestText").gameObject;
        Text questText = tempText.GetComponent<Text>();
        GameObject player = GameObject.Find("player");
        Camera cam = Camera.main;

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q1")
        {
            //GameObject master = GameObject.Find("master_of_dance");
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;

            for (int i = 0; i < itemLength; i++)
            {
                GameObject itemsQuest = transform.Find("menu/Item" + i).gameObject;
                itemsQuest.transform.SetParent(transform.Find("menu"));

                Destroy(itemsQuest);
            }
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q2")
        {
            //GameObject master = GameObject.Find("master_of_dance");
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;

            for (int i = 0; i < itemLength; i++)
            {
                GameObject itemsQuest = transform.Find("menu/Item" + i).gameObject;
                itemsQuest.transform.SetParent(transform.Find("menu"));

                Destroy(itemsQuest);
            }
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q3")
        {
            //GameObject master = GameObject.Find("master_of_dance");
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;

            for (int i = 0; i < itemLength; i++)
            {
                GameObject itemsQuest = transform.Find("menu/Items").gameObject;
                itemsQuest.transform.SetParent(transform.Find("menu"));

                Destroy(itemsQuest);
            }
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q4")
        {
            //GameObject master = GameObject.Find("master_of_dance");
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;

            for (int i = 0; i < itemLength; i++)
            {
                GameObject itemsQuest = transform.Find("menu/Items").gameObject;
                itemsQuest.transform.SetParent(transform.Find("menu"));

                Destroy(itemsQuest);
            }
        }
    }
}


