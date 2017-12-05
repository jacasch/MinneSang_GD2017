using UnityEngine;

[System.Serializable]
public class Item {
    public Item(GameObject drop, string name) {
        this.drop = drop;
        this.name = name;
    }

    public GameObject drop;
    public string name;
}
