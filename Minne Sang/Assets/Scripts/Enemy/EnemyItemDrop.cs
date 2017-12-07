using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDrop : MonoBehaviour {
    public int relatedAct;
    public string relatedQuest;
    public Item questDrop;

    private float spawnDelay = 10f;
    private GameObject player;
    private PlayerQuestHandler pq;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        pq = player.GetComponent<PlayerQuestHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Death() {
        //if in act
        if (pq.activeAct == relatedAct)
        {
            //if in quest
            if (pq.activeQuest == relatedQuest)
            {
                //if player has not item
                if (pq.HasItem(questDrop.name))
                {
                    DropItem();
                }
            }
            //else
            else
            {
                Respawn(spawnDelay);
            }
        }
    }

    void DropItem() {
        GameObject drop = Instantiate(questDrop.drop, transform.position, transform.rotation);
        drop.GetComponent<ItemHandler>().SetName(questDrop.name);
    }

    void Respawn(float seconds) {
        ///DO SOME SUTUFF HERE PLS MAGIC HAPPEN NOW!!
    }
}
