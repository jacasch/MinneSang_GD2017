using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMusicStomp : MonoBehaviour
{
    float liveTime = 1f;  //Wie lange die Druckwelle bestehen bleibt
    float stayTime = 0.75f;  //Dauer wie lange die Maximale Länge der Druckwelle bestehen bleibt
    float lengthMax = 0.095f;  //Maximale Reichweite

    //ScriptVariables
    float length = 0.04f;
    float heigth = 0.1f;
    float lengthDif;
    float lengthAdd;
    BoxCollider2D boxCol;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start ()
    {
        boxCol = GetComponent<BoxCollider2D>() as BoxCollider2D;
        boxCol.size = new Vector2(length,heigth);

        lengthDif = lengthMax - length;
        lengthAdd = lengthDif*(Time.deltaTime/(liveTime-stayTime));
    }
	
	// Update is called once per frame
	void Update ()
    {
        colSize();
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    //FUNCTIONS------------------------------------------------------------------------------------------------------------
    //Vergrössert den collider des Stomps
    void colSize()
    {
        if(length<lengthMax)
        {
            length += lengthAdd;
        }
        boxCol.size = new Vector2(length, heigth);
    }
}