using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class Explosion : MonoBehaviour {

    public GameObject FxExplo;
    public GameObject currentFx;
    private float ExplosionForce = 1;
    private float percentageOfExplosionsSound = 1;
    private float percentageOfExplosionsVisual = 3;
    public float lifeSpan = 5;
    public float maxLifeSpan = 15;
    private float startTimer;
    private bool startDying;
    private float point = 666;

    public AudioClip[] listExplosions;
    private AudioSource speakers;
    private ScoreManager scoring;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "Ground" && col.tag != "Building")
        {

            Destroy(gameObject,Random.Range(lifeSpan, lifeSpan*1.2f));
            scoring.AddScore(point);

            startTimer = Time.time;
            startDying = true;

            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-ExplosionForce, ExplosionForce) * 100, Random.Range(1, ExplosionForce) * 100, Random.Range(-ExplosionForce, ExplosionForce) * 100), ForceMode.Impulse) ;
            gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)), ForceMode.Impulse);
            GetComponent<Collider>().enabled = false;

            if (Random.Range(0, 100) < percentageOfExplosionsVisual)
            {
                currentFx = Instantiate(FxExplo, transform.position + transform.up * 60, Quaternion.identity);
                Destroy(currentFx, Random.Range(1f,1.5f));
            }

            if (Random.Range(0, 100) < percentageOfExplosionsSound)
            {
                speakers.enabled = true;
                speakers.loop = false;
                speakers.clip = listExplosions[Random.Range(0, listExplosions.Length)];
                speakers.Play();
            }
        }
    }

    void Start()
    {
        startTimer = Time.time;
        speakers = gameObject.GetComponent<AudioSource>();
        speakers.enabled = false;
        startDying = false;
        scoring = ScoreManager.instance;
        currentFx = null;
        Destroy(gameObject, Random.Range(maxLifeSpan, maxLifeSpan*1.2f));
    }
}
