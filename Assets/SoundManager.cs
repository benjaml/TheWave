using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public AudioSource failleSound;
    public AudioSource introSound;
    public AudioSource gameLoopSound;

    public void changeScene()
    {
        SceneManager.LoadScene("Main");
        gameLoopSound.Play();
    }
}
