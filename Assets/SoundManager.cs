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

        introSound.clip = menuAmbient;
        introSound.Play();
    }

    public AudioSource failleSound;
    public AudioSource introSound;
    public AudioSource gameLoopSound;
    public AudioSource explosionSound;

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
        }else if(sceneName == "EndMenu")
        {
            Fade.instance.FadeOut("EndMenu");
            gameLoopSound.clip = menu;
            gameLoopSound.Play();

            introSound.clip = menuAmbient;
            introSound.Play();
        }
        else if(sceneName == "FailleScene")
        {
            gameLoopSound.Stop();
            
            introSound.Stop();
        }
        
    }
}
