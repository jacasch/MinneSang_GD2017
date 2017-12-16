using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMusicStomp : MonoBehaviour
{
    float liveTime = 1f;  //Wie lange die Druckwelle bestehen bleibt
    float stayTime = 0.5f;  //Dauer wie lange die Maximale Länge der Druckwelle bestehen bleibt
    float lengthMax = 4.5f;  //Maximale Reichweite

    //ScriptVariables
    float length = 2.5f;
    //float heigth = 7f;
    float lengthDif;
    float lengthAdd;
    //BoxCollider2D boxCol;
    CircleCollider2D circleCol;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start ()
    {
        //boxCol = GetComponent<BoxCollider2D>() as BoxCollider2D;
        //boxCol.size = new Vector2(length,heigth);

        circleCol = GetComponent<CircleCollider2D>() as CircleCollider2D;
        circleCol.radius = length;

        lengthDif = lengthMax - length;
        lengthAdd = lengthDif*(Time.deltaTime/(liveTime-stayTime));
    }
	
	// Update is called once per frame
	void Update ()
    {
        colSize();
        liveTime -= Time.deltaTime;
        if (length >= lengthMax)
        {
            circleCol.radius = 1;
        }
        if(liveTime<0)
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
        //boxCol.size = new Vector2(length, heigth);
        circleCol.radius = length;
    }
}