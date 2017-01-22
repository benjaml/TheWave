using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Fade : MonoBehaviour {

    public static Fade instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public CanvasGroup canvas;
    public float fadeDuration;

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
        FadeIn();
    }

    public void FadeIn()
    {
        canvas.alpha = 1f;
        canvas.DOFade(0.0f, fadeDuration);
    }

    public void FadeOut(string sceneName = null)
    {
        canvas.alpha = 0.0f;
        if (sceneName != null)
        {
            canvas.DOFade(1.0f, fadeDuration).OnComplete(() => SceneManager.LoadScene(sceneName));
        }else
        {
            canvas.DOFade(1.0f, fadeDuration);
        }
        
    }
}
