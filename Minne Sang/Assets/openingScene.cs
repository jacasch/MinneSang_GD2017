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

    // Use this for initialization
    void Start () {
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
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
