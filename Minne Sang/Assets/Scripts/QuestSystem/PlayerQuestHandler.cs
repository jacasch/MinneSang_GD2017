using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestHandler : MonoBehaviour {
    public string activeQuest;
    public int activeAct;
    public List<string> missingItems = new List<string>();
    public List<string> collectedItems = new List<string>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent.gameObject.tag == "Item")
        {
            AddItem(collision.gameObject.transform.parent.gameObject.GetComponent<ItemHandler>().GetName());
            print("picked up");
            Destroy(collision.gameObject.transform.parent.gameObject);
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
}
