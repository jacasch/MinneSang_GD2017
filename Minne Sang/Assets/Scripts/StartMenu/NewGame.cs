using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

    public GameObject playerPackage;
    GameObject player;
    PlayerQuestHandler pqh;
    PlayerSpawnHandler psh;

    private void OnMouseDown()
    {
        StartNewGame();
    }

    private void StartNewGame() {

        GameObject temp = Instantiate(playerPackage, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        player = temp.transform.Find("player").gameObject;
        pqh = player.GetComponent<PlayerQuestHandler>();
        psh = player.GetComponent<PlayerSpawnHandler>();

        pqh.activeAct = -1;
        pqh.activeQuest = "mastered";
        pqh.letters = 0;
        pqh.letterSend = false;     
        psh.targetScene = "Intro";
        psh.targetSpawn = "start";

        psh.Respawn();
    }
}
