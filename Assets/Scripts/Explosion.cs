using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class Explosion : MonoBehaviour {

    public float ExplosionForce;
    public int maxRebound = 3;
    private int currentRebound;
    public float percentageOfExplosions = 10;
    public float lifeSpan = 5;
    private float startTimer;
    private bool startDying;
    public float point = 1000;

    public AudioClip[] listExplosions;
    private AudioSource speakers;
    private ScoreManager scoring;

    void OnCollisionEnter(Collision col)
    {
        if (maxRebound != 0 && currentRebound >= maxRebound)
            return;

        if (col.collider.tag != "Ground" && col.collider.tag != "Building")
        {
            Vector3 average = Vector3.zero;

            for (int i = 0; i < col.contacts.Length; i++)
            {
                average += col.contacts[i].point;
            }

            average /= col.contacts.Length;

            gameObject.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce * 100, average, 10.0f);

            if(!startDying)
            {
                scoring.AddScore(point);
            }

            startTimer = Time.time;
            startDying = true;
            
            if (Random.Range(0, 100) < percentageOfExplosions)
            {
                speakers.loop = false;
                speakers.clip = listExplosions[Random.Range(0, listExplosions.Length)];
                speakers.Play();
            }
        }

        currentRebound++;
    }

    void Awake()
    {
        currentRebound = 0;
        startTimer = 0;
        speakers = gameObject.GetComponent<AudioSource>();
        startDying = false;
        scoring = GameObject.Find("Manager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (startDying && lifeSpan + startTimer < Time.time)
            Destroy(gameObject);
    }
}
