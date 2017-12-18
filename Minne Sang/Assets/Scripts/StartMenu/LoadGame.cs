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

        psh.Respawn();
    }
}
