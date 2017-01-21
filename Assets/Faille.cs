using System.Collections;
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

    // Use this for initialization
    void Start () {
        failleSound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        charge = Mathf.Max(0, charge - (charge*0.1f) * Time.deltaTime);
        if (Input.anyKeyDown)
        {
            bubules[Random.Range(0, bubules.Length)].Play();
            charge += 1f;
            lastInput = Time.time;
        }
        float ratio = charge / chargeMax;
        Camera.main.transform.DOShakePosition(1f, (0.2f * (ratio))).OnComplete(() => Camera.main.transform.DOKill());
        failleSound.volume = ratio;
        text.transform.DOShakePosition(1f, (1)).OnComplete(() => text.DOKill());
        text.color = new Color(1, 1-ratio, 1-ratio);
	}
}
