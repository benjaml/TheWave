using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class Explosion : MonoBehaviour {

    public GameObject FxExplo;
    public GameObject currentFx;
    public float ExplosionForce;
    public int maxRebound = 3;
    private int currentRebound;
    public float percentageOfExplosionsSound = 5;
    public float percentageOfExplosionsVisual = 60;
    public float lifeSpan = 5;
    public float maxLifeSpan = 15;
    private float startTimer;
    private bool startDying;
    public float point = 1000;

    public AudioClip[] listExplosions;
    private AudioSource speakers;
    private ScoreManager scoring;

    void OnTriggerEnter(Collider col)
    {
        if (maxRebound != 0 && currentRebound >= maxRebound)
            return;

        if (col.tag != "Ground" && col.tag != "Building")
        {
            scoring.AddScore(point);
            Destroy(gameObject,lifeSpan);

            startTimer = Time.time;
            startDying = true;

            if (Random.Range(0, 100) < percentageOfExplosionsVisual)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-ExplosionForce, ExplosionForce) * 100, Random.Range(-ExplosionForce, ExplosionForce) * 100, Random.Range(-ExplosionForce, ExplosionForce) * 100), ForceMode.Impulse);
                currentFx = Instantiate(FxExplo, transform.position, Quaternion.identity);
                Destroy(currentFx, lifeSpan);
            }

            if (Random.Range(0, 100) < percentageOfExplosionsSound)
            {
                speakers.enabled = true;
                speakers.loop = false;
                speakers.clip = listExplosions[Random.Range(0, listExplosions.Length)];
                speakers.Play();
            }
        }

        currentRebound++;
    }

    void Start()
    {
        currentRebound = 0;
        startTimer = Time.time;
        speakers = gameObject.GetComponent<AudioSource>();
        speakers.enabled = false;
        startDying = false;
        scoring = ScoreManager.instance;
        currentFx = null;
        Destroy(gameObject, maxLifeSpan);
    }
}
