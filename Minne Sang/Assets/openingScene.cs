using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class openingScene : MonoBehaviour {
    private VideoPlayer video;
    private VideoClip clip;
    public AudioClip videoAudio;
    private Camera cam;
    private int count = 0;

    public bool endScene = false;
    public bool credits = false;

    GameObject player;

    // Use this for initialization
    void Start () {
        if (GameObject.FindWithTag("Player") != null) {
            player = GameObject.FindWithTag("Player").transform.parent.gameObject;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            Destroy(player);
        }
        cam = Camera.main;

        count += 1;
        video = cam.GetComponent<VideoPlayer>();
		if (count > 100 && !video.isPlaying)
        {
            if (endScene)
            {
                SceneManager.LoadScene("Credits", LoadSceneMode.Single);
            }
            else { 
            SceneManager.LoadScene("Intro", LoadSceneMode.Single);
            }

            if(credits)
            {
                SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
            }
        }

        if (Input.anyKey)
        {
            if (endScene)
            {
                SceneManager.LoadScene("Credits", LoadSceneMode.Single);
            }
        else
        {
            SceneManager.LoadScene("Intro", LoadSceneMode.Single);
        }
            if (credits)
            {
                SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
            }
        }
    }
}
