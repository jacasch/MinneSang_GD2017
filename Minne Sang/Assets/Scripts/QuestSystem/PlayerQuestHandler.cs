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
    public bool questEnabled = false;

    Vector3 Vector3 = new Vector3(Screen.width/2, Screen.height/2);
    //    public bool optionToSendLetters = false;
    public List<string> missingItems = new List<string>();
    public List<string> collectedItems = new List<string>();
    public List<string> questItems = new List<string>();

    private AudioSource sourceDrop;
    private float currCountdownValue;
    private int down = 0;


    // Use this for initialization
    void Start()
    {
        questEnabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        menuHandler();
        questAlarm();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent != null)
        {
            if (collision.gameObject.transform.parent.gameObject.tag == "Item")
            {
                //Play Audio
                sourceDrop = GetComponent<AudioSource>();
                AudioClip clip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sounds/Items/PickupItem.wav");
                sourceDrop.PlayOneShot(clip);

                // Pickup
                AddItem(collision.gameObject.transform.parent.gameObject.GetComponent<ItemHandler>().GetName());
                print("picked up");
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
        }
    }

    public void questAlarm()
    {
        GameObject questAL = transform.Find("canvasQuest").gameObject; 
        Canvas canvasQuest = questAL.GetComponent<Canvas>();
        GameObject player = GameObject.Find("player");
        AudioSource source = questAL.GetComponent<AudioSource>();
        AudioClip clip = source.clip;

        if (questEnabled)
            {
            down++;

            if (down == 1) { 
                source.PlayOneShot(clip);
            }


            //Debug.Log("got Quest");
            canvasQuest.enabled = true;
            //QuestCountdown();
            if (down > 800 || Input.GetKeyDown("joystick 1 button 7") || Input.GetKeyDown("m")) // || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick 1 button 0"))
            {
                down = 0;
                questEnabled = false;
                canvasQuest.enabled = false;
            }
        } else
        {
            canvasQuest.enabled = false;
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
        GUIStyle textStyle = new GUIStyle();
        string itemColor = "";

        string itemQuest = "";

        // START
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "mastered" && player.GetComponent<PlayerQuestHandler>().activeAct == -1)
        {
            questText.text = "You came to Tonilot to master the high arts. To accomplish this task you must speak with the Headmaster first.";
        }

        // After Skill Mastery
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "mastered" && player.GetComponent<PlayerQuestHandler>().activeAct == 0)
        {
            questText.text = "Master the swan dash and escape the dungeon! Use (B) to dash. You can use this to avoid the exploding dancers and cover great distances. But beware! The dash can't be used more than two times in succession.";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "mastered" && player.GetComponent<PlayerQuestHandler>().activeAct == 1)
        {
            questText.text = "Colour bombs can show you things you could not see before. You will need to practise to be able to escape this dungeon. Hold (Y) to aim and release it to throw the colour bombs.";
        }


        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "mastered" && player.GetComponent<PlayerQuestHandler>().activeAct == 2)
        {
            questText.text = "Play the powerful trumpet to break barriers and send ogres to sleep temporarily. Hold (RB) to play your tunes.";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "mastered" && player.GetComponent<PlayerQuestHandler>().activeAct == 3)
        {
            questText.text = "With the poetic manipulation of words, you can now shield yourself from the violent shrieks of the dreaded sirene. Hold (LB) to activate the aura of self respect.";
        }

        // After End
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "end" || player.GetComponent<PlayerQuestHandler>().activeAct == 4)
        {
            questText.text = "You have mastered all the arts! You have never been closer to obtaining the love of your dear princess. But you are missing one thing. Your final task is to obtain the unobtainable. The tower challenges you.";
        }

        // After talking to Headmaster in different Acts
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "running" && player.GetComponent<PlayerQuestHandler>().activeAct == 0)
        {
            questText.text = "You've met the headmaster, who sent you off to meet the high masters. Onwards, to master the arts and finally true love!";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "running" && player.GetComponent<PlayerQuestHandler>().activeAct == 1)
        {
            questText.text = "You have mastered the art of dance. For your next task, the fine arts!";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "running" && player.GetComponent<PlayerQuestHandler>().activeAct == 2)
        {
            questText.text = "You have mastered the fine arts and dance. Your next challenge will be the art of music!";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "running" && player.GetComponent<PlayerQuestHandler>().activeAct == 3)
        {
            questText.text = "Three high arts mastered, one left to go. It's time to ask yourself, if poetry really is the art of seduction?";
        }

        // During different Quests
        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q1")
        {
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;

            for (int i = 0; i < itemLength; i++)
            {

                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    itemColor = "<color=#bca655FF>";
                }
                else
                {
                    itemColor = "<color=#000000ff>";
                }

                itemQuest = itemQuest +"\n"+itemColor+questItems[i]+"</color>";

                questText.text = "The high master of dance has lost his clothes. You must take to him his tiara, tutu and shoes, so that he may instruct you in the art of dance. Students in the dance area or main hall may have them. Look around and talk.\n" + itemQuest;
            }
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q2")
        {
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;

            for (int i = 0; i < itemLength; i++)
            {
                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    itemColor = "<color=#bca655FF>";
                }
                else
                {
                    itemColor = "<color=#000000ff>";
                }

                itemQuest = itemQuest + "\n" + itemColor + questItems[i] + "</color>";
            }
            questText.text = "You must craft a brush from the finest horse hair and the best wood you can find. The music area might have what you are looking for but you doubt that there are horses inside of the castle.\n" + itemQuest;
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q3")
        {
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;
            int numberOfItems = 0;

            for (int i = 0; i < itemLength; i++)
            {
                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    numberOfItems += 1;
                }
            }

            if (numberOfItems == itemLength)
            {
                itemColor = "<color=#bca655FF>";
            }
            else
            {
                itemColor = "<color=#000000ff>";
            }

            questText.text = "The high master of music lost his music sheets to the devious dancers and one of his absent minded students. Retrieve them in the music area.\n\n" + itemColor+"Music sheets to collect: " + numberOfItems + "/" + itemLength + "</color>";
        }

        if (player.GetComponent<PlayerQuestHandler>().activeQuest == "q4")
        {
            int itemLength = player.GetComponent<PlayerQuestHandler>().questItems.Count;
            int numberOfItems = 0;

            for (int i = 0; i < itemLength; i++)
            {
                if (collectedItems.Contains(player.GetComponent<PlayerQuestHandler>().questItems[i]))
                {
                    numberOfItems += 1;
                }
            }


            if (numberOfItems == itemLength)
            {
                itemColor = "<color=#bca655FF>";
            }
            else
            {
                itemColor = "<color=#000000ff>";
            }

            questText.text = "The high master of poetry needs ink to finish his poem. Only then will he be able to teach you how to be a wordsmith. Milk some squids an bring him the liquid of his desire!\n\n" + itemColor+"Ink to collect: " + numberOfItems + " / " + itemLength + "</color>";
        }
    }


    // Redraw Menus on Input
    public void menuTextQuestDestroy()
    {
        
    }
}


