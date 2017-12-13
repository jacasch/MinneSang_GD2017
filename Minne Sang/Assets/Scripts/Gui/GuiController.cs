using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GuiController : MonoBehaviour {

    public Sprite backgroundSprite;
    public Sprite[] healthSprite = new Sprite[8];
    public Sprite[] skillSprite = new Sprite[5];
    public Sprite[] dashSprite = new Sprite[3];

    private PlayerGui pg;
    private SpriteRenderer backgroundRenderer;
    private SpriteRenderer healthRenderer;
    private SpriteRenderer skillRenderer;
    private SpriteRenderer dashRenderer;

    // Use this for initialization
    void Start () {
        pg = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGui>();
        backgroundRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        healthRenderer = transform.Find("Health").GetComponent<SpriteRenderer>();
        skillRenderer = transform.Find("Skill").GetComponent<SpriteRenderer>();
        dashRenderer = transform.Find("Dash").GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        //fatching and clamping data;
        int health = Mathf.RoundToInt(pg.health);
        health = (health < 0) ? 0 : health;
        health = (health > 7) ? 7 : health;
        int skillLevel = Mathf.RoundToInt(pg.skillLevel);
        skillLevel = (skillLevel < 0) ? 0 : skillLevel;
        skillLevel = (skillLevel > 4) ? 4 : skillLevel;
        int dashesLeft = pg.dashesLeft;
        dashesLeft = (dashesLeft < 0) ? 0 : dashesLeft;
        dashesLeft = (dashesLeft > 2) ? 2 : dashesLeft;


        healthRenderer.sprite = healthSprite[health];
        skillRenderer.sprite = skillSprite[skillLevel];
        dashRenderer.sprite = dashSprite[dashesLeft];
    }
}
