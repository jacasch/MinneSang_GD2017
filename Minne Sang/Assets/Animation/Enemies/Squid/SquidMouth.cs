using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidMouth : MonoBehaviour
{
    public GameObject objEnemy;
    EnemyPaint enemyPaint;

    SpriteRenderer mySprite;

    // Use this for initialization
    void Start ()
    {
		enemyPaint = objEnemy.GetComponent<EnemyPaint>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (enemyPaint.dir < 0)
        {

        }
        else
        {

        }
	}
}
