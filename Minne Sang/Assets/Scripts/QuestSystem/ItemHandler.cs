using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {
    public string name = "Item Template";

    private float pickUpDelay = 1f;

    private void Start()
    {
        transform.Find("trigger").gameObject.layer = 8;
    }

    private void Update()
    {   if (pickUpDelay >= 0)
        {
            pickUpDelay -= Time.deltaTime;
        }
        else
        {
            transform.Find("trigger").gameObject.layer = 0;
        }
    }

    public void SetName(string name) {
        this.name = name;
    }
    public string GetName() {
        return name;
    }
}
