﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {
    private string name = "Item Template";
    public void SetName(string name) {
        this.name = name;
    }
    public string GetName() {
        return name;
    }
}