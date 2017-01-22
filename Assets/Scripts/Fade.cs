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

    public GameObject canvas;
    public CanvasGroup canvasGroup;
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
        canvas.SetActive(true);
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0.0f, fadeDuration).OnComplete(() => canvas.SetActive(false));
    }

    public void FadeOut(string sceneName = null)
    {
        canvas.SetActive(true);
        canvasGroup.alpha = 0.0f;
        if (sceneName != null)
        {
            canvasGroup.DOFade(1.0f, fadeDuration).OnComplete(() => SceneManager.LoadScene(sceneName));
        }else
        {
            canvasGroup.DOFade(1.0f, fadeDuration);
        }
        
    }
}
