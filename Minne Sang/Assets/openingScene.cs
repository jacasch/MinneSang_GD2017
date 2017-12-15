using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class openingScene : MonoBehaviour {
    VideoPlayer video;
    VideoClip clip;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (video.isPlaying == false)
        {
            SceneManager.LoadScene("Intro", LoadSceneMode.Single);
        }
    }
}
