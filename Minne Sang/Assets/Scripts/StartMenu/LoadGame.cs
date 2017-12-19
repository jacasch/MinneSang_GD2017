using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

    public GameObject playerPackage;
    GameObject player;
    PlayerQuestHandler pqh;
    PlayerSpawnHandler psh;

    private void OnMouseDown()
    {
        LoadPreviousGame();
    }

    public void LoadPreviousGame()
    {
        GameObject temp = Instantiate(playerPackage,new Vector3(0,0,0), Quaternion.Euler(0,0,0));
        player = temp.transform.Find("player").gameObject;
        pqh = player.GetComponent<PlayerQuestHandler>();
        psh = player.GetComponent<PlayerSpawnHandler>();

        pqh.activeAct = PlayerPrefs.GetInt("act");
        pqh.activeQuest = PlayerPrefs.GetString("quest");
        pqh.letters = PlayerPrefs.GetInt("letters");
        pqh.letterSend = PlayerPrefs.GetInt("send") == 0 ? false : true;
        psh.targetScene = PlayerPrefs.GetString("scene");
        psh.targetSpawn = PlayerPrefs.GetString("spawn");

        //load questitems
        int count = PlayerPrefs.GetInt("questItemAmount");
        for (int i = 0; i < count; i++)
        {
            pqh.questItems.Add(PlayerPrefs.GetString("questItem" + i.ToString()));
        }
        //load collected items
        int collcount = PlayerPrefs.GetInt("collectedItemAmount");
        for (int i = 0; i < collcount; i++)
        {
            pqh.collectedItems.Add(PlayerPrefs.GetString("collectedItem" + i.ToString()));
        }

        psh.Respawn();
    }

    public void LoadGameAct(int act)
    {
        string[] scenes = new string[] {"DanceArea", "PaintingArea", "MusicArea", "PoetryArea", "FinalArea"};
        string[] doors = new string[] { "MainHallDance", "MainHallDoorPainting", "MainHallDoorMusic", "MainHallDoorPoetry", "MainhallDoorFinal" };

        GameObject temp = Instantiate(playerPackage, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        player = temp.transform.Find("player").gameObject;
        pqh = player.GetComponent<PlayerQuestHandler>();
        psh = player.GetComponent<PlayerSpawnHandler>();

        pqh.activeAct = act - 1;
        pqh.activeQuest = act == 5 ? "mastered" : "running";
        pqh.letters = act - 1;
        pqh.letterSend = act == 5 ? true : false;
        psh.targetScene = scenes[act - 1];
        psh.targetSpawn = doors[act -1 ];

        psh.Respawn();
    }
}
