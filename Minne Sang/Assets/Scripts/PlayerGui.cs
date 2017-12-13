using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGui : MonoBehaviour {
    public int dashesLeft;
    public float health;
    public float maxhealth;
    public float skillLevel;
    public List<string> items;

    private PlayerController pc;
    private PlayerStats ps;
    private PlayerQuestHandler pqh;

    private void Start()
    {
        pc = GetComponent<PlayerController>();
        ps = GetComponent<PlayerStats>();
        pqh = GetComponent<PlayerQuestHandler>();
    }

    private void Update()
    {
        skillLevel = pqh.activeQuest == "mastered" ? pqh.activeAct + 1  : pqh.activeAct;

        dashesLeft = pc.dashesLeft;

        health = ps.hp;
        maxhealth = ps.maxHP;

        items = pqh.collectedItems;
    }
}
