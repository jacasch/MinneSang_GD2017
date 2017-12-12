using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidMouth : MonoBehaviour
{
    float animTimer = 0;

    public GameObject objEnemy;
    EnemyPaint enemyPaint;

    SpriteRenderer mySprite;
    public Material defaultMat;
    public Material chameleonMat;

    Animator animator;

    // Use this for initialization
    void Start ()
    {
		enemyPaint = objEnemy.GetComponent<EnemyPaint>();
        mySprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (enemyPaint.stealth)
        {
            mySprite.material = chameleonMat;
        }
        else
        {
            mySprite.material = defaultMat;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        print(enemyPaint.dir);
        if (enemyPaint.dir > 0 && !enemyPaint.stealth)
        {
            mySprite.flipX = true;
        }
        else
        {
            mySprite.flipX = false;
        }
        if(enemyPaint.shootTimer<=0)
        {
            animator.SetBool("shoot", true);
            animTimer = 0.1f;
        }
        if(animTimer<=0)
        {
            animator.SetBool("shoot", false);
        }
        else
        {
            animTimer -= Time.deltaTime;
        }
        if (enemyPaint.stealth)
        {
            mySprite.material = chameleonMat;
        }
        else
        {
            mySprite.material = defaultMat;
        }
    }
}
