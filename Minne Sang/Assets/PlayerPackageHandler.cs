using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPackageHandler : MonoBehaviour {
    void Start() {
        DontDestroyOnLoad(gameObject);

        //if there is another player in the scen destroy it
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerPackage");
        foreach (GameObject p in players)
        {
            if (p != gameObject)
            {
                Destroy(gameObject);
            }
        }
    }
}
