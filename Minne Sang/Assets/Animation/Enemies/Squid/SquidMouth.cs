using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidMouth : MonoBehaviour
{
    float animTimer = 0;

    public GameObject objEnemy;
    EnemyPaint enemyPaint;

    Animator animator;

    // Use this for initialization
    void Start ()
    {
		enemyPaint = objEnemy.GetComponent<EnemyPaint>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
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
    }
}
