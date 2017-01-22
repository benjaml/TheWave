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

    void Start()
    {
        gameLoopSound.clip = menu;
        gameLoopSound.Play();

        ambientSound.clip = menuAmbient;
        ambientSound.Play();
    }

    public AudioSource failleSound;
    public AudioSource introSound;
    public AudioSource gameLoopSound;
    public AudioSource explosionSound;
    public AudioSource ambientSound;

    public AudioClip game;
    public AudioClip menu;
    public AudioClip menuAmbient;

    public void changeScene(string sceneName)
    {
        if(sceneName == "Main")
        {
            Fade.instance.FadeOut("Main");
            gameLoopSound.clip = game;
            gameLoopSound.Play();
        }else if(sceneName == "EndScreen")
        {
            gameLoopSound.clip = menu;
            gameLoopSound.Play();

            ambientSound.clip = menuAmbient;
            ambientSound.Play();
        }
        else if(sceneName == "FailleScene")
        {
            gameLoopSound.Stop();

            ambientSound.Stop();
        }

    }
}
