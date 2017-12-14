using System.Collections;
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

    Vector3 Vector3 = new Vector3(Screen.width/2, Screen.height/2);
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
        Vector3 = new Vector3(Screen.width / 2, Screen.height / 2);
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
        GameObject tempObject = transform.Find("menu").gameObject;
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
                //Time.timeScale = 1.0f;
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
        int test = cam.pixelWidth / 2;

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
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;

            for (int i = 0; i < itemLength; i++)
            {
                GameObject itemsQuest = new GameObject("Item" + i);
                itemsQuest.transform.SetParent(transform.Find("menu/Panel"));

                Text text = itemsQuest.AddComponent<Text>();
                // GET BUILT IN WRONG??
                text.font = (Font)Resources.Load("Fonts/CalvertMTStd");
                text.text = questItems[i];
                text.fontSize = 18;
                text.alignment = TextAnchor.LowerCenter;
                text.transform.localPosition = new Vector2(-10, -(test/2-90 - i * 30));
                text.transform.localScale = new Vector3(1, 1, 1);

                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    text.color = Color.red;
                }
                else
                {
                    text.color = Color.black;
                }
            }
            questText.text = "The high master of dance lost all his clothes. Bring him his tiara, his tutu and shoes and maybe he will instruct you in the art of dance. He told you to ask one his students or look around the mainhall.";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q2")
        {
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;

            for (int i = 0; i < itemLength; i++)
            {
                GameObject itemsQuest = new GameObject("Item" + i);
                itemsQuest.transform.SetParent(transform.Find("menu/Panel"));

                Text text = itemsQuest.AddComponent<Text>();
                // GET BUILT IN WRONG??
                text.font = (Font)Resources.Load("Fonts/CalvertMTStd");
                text.text = questItems[i];
                text.fontSize = 18;
                text.alignment = TextAnchor.LowerCenter;
                text.transform.localPosition = new Vector2(-10, -(test / 2 - 90 - i * 30));
                text.transform.localScale = new Vector3(1, 1, 1);

                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    text.color = Color.red;
                }
                else
                {
                    text.color = Color.black;
                }
            }
            questText.text = "The fine arts master is in need of a new brush. Craft him one from the finest horse hair and a splendid piece of wood!";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q3")
        {
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;
            int numberOfItems = 0;

            GameObject itemsQuest = new GameObject("Items");
            itemsQuest.transform.SetParent(transform.Find("menu/Panel"));

            for (int i = 0; i < itemLength; i++)
            {
                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    numberOfItems += 1;
                }
            }


            Text text = itemsQuest.AddComponent<Text>();
            text.font = (Font)Resources.Load("Fonts/CalvertMTStd");
            text.text = "Music Sheets: " + numberOfItems + "/" + itemLength;
            text.fontSize = 18;
            text.alignment = TextAnchor.LowerCenter;
            text.transform.localPosition = new Vector2(-10, -(test/2-90));
            text.transform.localScale = new Vector3(1, 1, 1);

            if (numberOfItems == itemLength)
            {
                text.color = Color.red;
            }
            else
            {
                text.color = Color.black;
            }

            questText.text = "The high master of music lost his music sheets to the devious dancers and one of his absent minded students. Retrieve them!";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q4")
        {
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;
            int numberOfItems = 0;

            GameObject itemsQuest = new GameObject("Items");
            itemsQuest.transform.SetParent(transform.Find("menu/Panel"));

            for (int i = 0; i < itemLength; i++)
            {
                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    numberOfItems += 1;
                }
            }


            Text text = itemsQuest.AddComponent<Text>();
            text.font = (Font)Resources.Load("Fonts/CalvertMTStd");
            text.text = "Ink collected: " + numberOfItems + "/" + itemLength;
            text.fontSize = 18;
            text.alignment = TextAnchor.LowerCenter;
            text.transform.localPosition = new Vector2(-10, -(test / 2 - 90));
            text.transform.localScale = new Vector3(1, 1, 1);


            if (numberOfItems == itemLength)
            {
                text.color = Color.red;
            }
            else
            {
                text.color = Color.black;
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
                GameObject itemsQuest = transform.Find("menu/Panel/Item" + i).gameObject;
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
                GameObject itemsQuest = transform.Find("menu/Panel/Item" + i).gameObject;
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
                GameObject itemsQuest = transform.Find("menu/Panel/Items").gameObject;
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
                GameObject itemsQuest = transform.Find("menu/Panel/Items").gameObject;
                itemsQuest.transform.SetParent(transform.Find("menu"));

                Destroy(itemsQuest);
            }
        }
    }
}


