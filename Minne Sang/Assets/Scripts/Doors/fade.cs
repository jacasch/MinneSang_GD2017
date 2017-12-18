using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade : MonoBehaviour {

    private float fadeSpeed;
    private Image panel;

    // Use this for initialization
    void Start () {
       // Image panel = GetComponent<Image>();
        fadeSpeed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        fadeSpeed -= 0.01f;
        if (fadeSpeed <= 0) {
            fadeSpeed = 0;
        }

        if (fadeSpeed >= 1)
        {
            fadeSpeed = 1;
            fadeIn();
        }
        Debug.Log(fadeSpeed);
        Image panel = GetComponent<Image>();
        //GameObject fade = transform.Find("Main Camera/Canvas/Fade").gameObject;
        panel.color = fader();
	}

    public bool fadeIn()
    {
        fadeSpeed = 0.000001f;
        fadeSpeed += 0.01f;

        if(fadeSpeed >= 1)
        {
            Debug.Log("Test");
            return true;
        } else
        {
            return false;
        }
    }

    Color fader()
    {
        return new Color(0, 0, 0, fadeSpeed);
    }
}
