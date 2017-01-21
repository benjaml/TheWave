using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float ExplosionForce;
    public int maxRebound = 3;
    private int currentRebound;

    void OnCollisionEnter(Collision col)
    {
        if (maxRebound != 0 && currentRebound >= maxRebound)
            return;

        Vector3 average = Vector3.zero;

        for(int i = 0; i < col.contacts.Length; i++)
        {
            average += col.contacts[i].point;
        }

        average /= col.contacts.Length;

        if(col.collider.tag != "Ground")
            gameObject.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce * 100, average, 10.0f);

        currentRebound++;
    }

    void Awake()
    {
        currentRebound = 0;
    }
}
