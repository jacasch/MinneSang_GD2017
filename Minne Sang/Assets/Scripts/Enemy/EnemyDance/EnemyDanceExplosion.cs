using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDanceExplosion : MonoBehaviour
{
    float timer = 0.5f;
    public float timeUntilDestroyed = 5f;

    public GameObject spriteExplosion;

    CircleCollider2D bombColl;

    SpriteRenderer boomSprite;

    //MAIN-----------------------------------------------------------------------------------------------------------------
    void Start ()
    {
        gameObject.layer = 0;
        bombColl = GetComponent<CircleCollider2D>();
        boomSprite = spriteExplosion.GetComponent<SpriteRenderer>();
    }
	
	void Update ()
    {
        timer -= Time.deltaTime;
        if(timer < 0.4f)
        {
            boomSprite.enabled = false;
        }
        if(timer<0)
        {
            bombColl.enabled = false;
        }
        if(timer < -timeUntilDestroyed)
        {
            Destroy(gameObject);
        }
	}
}
