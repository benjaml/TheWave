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
    public AudioSource explosionSound;

    public void changeScene()
    {
        Fade.instance.FadeOut("Main");
        gameLoopSound.Play();
    }
}
