﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Faille : MonoBehaviour {

    public ParticleSystem[] bubules;
    AudioSource failleSound;
    float lastInput = 0;
    public float delayToMute;
    public float charge = 0;
    public float chargeMax = 250;

    public GameObject leftFaille;
    public GameObject rightFaille;
    public Text text;
    public Text timer;

    bool started = false;

    // Use this for initialization
    void Start () {
        failleSound = GetComponent<AudioSource>();
        startSequence();
    }
	
	// Update is called once per frame
	void Update () {
        if (!started)
            return;
        charge = Mathf.Max(0, charge - (charge*0.1f) * Time.deltaTime);
        if (Input.anyKeyDown)
        {
            int nbBubule = Mathf.Max(1, (int)(charge / 10));
            for(int i = 0; i< nbBubule;i++)
                bubules[Random.Range(0, bubules.Length)].Play();
            charge += 1f;
            lastInput = Time.time;
        }
        float ratio = charge / chargeMax;
        Camera.main.transform.DOShakePosition(0.5f, (0.05f * (ratio))).OnComplete(() => Camera.main.transform.DOKill());
        failleSound.volume = ratio;
        text.transform.DOShakePosition(0.5f, (5f * (ratio))).OnComplete(() => text.transform.DOKill());
        text.color = new Color(1, 1-ratio, 1-ratio);
	}

    void startSequence()
    {
        resetTimer("" + 3);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(timer.transform.DOScale(1.5f, 1f))
        .Join(timer.DOFade(1f, 1f))
        .Append(timer.DOFade(0f, 0.5f))
        .Join(timer.transform.DOScale(0f, 0.5f).OnComplete(() => timer.text = "" + 2))
        .Append(timer.transform.DOScale(1.5f, 0.5f))
        .Join(timer.DOFade(1f, 0.5f))
        .Append(timer.DOFade(0f, 0.5f))
        .Join(timer.transform.DOScale(0f, 0.5f).OnComplete(() => timer.text = "" + 1))
        .Append(timer.transform.DOScale(1.5f, 0.5f))
        .Join(timer.DOFade(1f, 0.5f))
        .Append(timer.DOFade(0f, 0.5f))
        .Join(timer.transform.DOScale(0f, 0.5f).OnComplete(() => timer.text = "GO !"))
        .Append(timer.transform.DOScale(1.5f, 0.5f))
        .Join(timer.DOFade(1f, 0.5f).OnComplete(() => startGame()))
        .Append(timer.DOFade(0f, 0.5f))
        .Play();
    }
    void resetTimer(string s)
    {
        timer.text = s;
        timer.color = timer.color - new Color(0, 0, 0, 1);
        timer.transform.localScale = Vector3.zero;
    }

    void startGame()
    {
        started = true;
        failleSound.Play();
        Destroy(timer.gameObject);
    }
}
