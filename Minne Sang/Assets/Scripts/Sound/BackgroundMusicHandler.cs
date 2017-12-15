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
    }

    private void OnLevelWasLoaded(int level)
    {
        print("level loaded, scanning for music");
        string sceneName = SceneManager.GetActiveScene().name;
        if (!currentSong.sceneName.Contains(sceneName)) {
            foreach (Music song in music) {
                if (song.sceneName.Contains(sceneName)){
                    print("song found");
                    //change song
                    currentSong = song;
                    audioSource.Stop();
                    audioSource.clip = currentSong.clip;
                    audioSource.Play();
                    break;
                }
            }
        } //else keep playing the current song
        print("no song found");
    }

}
