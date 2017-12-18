using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicHandler : MonoBehaviour {
    [System.Serializable]
    public struct Music {
        [SerializeField]
        public AudioClip clip;
        [SerializeField]
        public List<string> sceneName;
    }

    public Music[] music;
    private Music currentSong;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        currentSong = music[0];
        UpdateMusic();
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        UpdateMusic();
    }

    private void UpdateMusic()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (currentSong.Equals(null))
            return;
        if (currentSong.sceneName == null)
            return;
        if (currentSong.clip == null)
            return;

        if (currentSong.sceneName.Contains(sceneName))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.clip = currentSong.clip;
                audioSource.Play();
            }
        }
        else
        {
            //search for new song
            foreach (Music song in music)
            {
                foreach (string name in song.sceneName)
                {
                    print(name);
                }
                if (song.sceneName.Contains(sceneName))
                {
                    //change song
                    currentSong = song;
                    audioSource.Stop();
                    audioSource.clip = currentSong.clip;
                    audioSource.Play();
                    break;
                }
            }
        } //else keep playing the current song
    }
}
