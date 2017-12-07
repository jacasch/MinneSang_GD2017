using UnityEngine;

[System.Serializable]
public class Item {
    public GameObject drop;
    public string name;

    public Item(GameObject drop, string name)
    {
        this.drop = drop;
        this.name = name;
    }
}
