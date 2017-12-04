using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDMG : MonoBehaviour
{
    public float dmg;  //Schaden der verursacht wird
    public float dmgTime;  //Dauer bis erneut Schaden verursacht wird.
    public float knockback;  //Stärke des Knockbacks

    public float timer;

    void Update()
    {
        if(timer>=0)
        {
            timer--;
        }
    }
}
