using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float speed;
    private int index = 0;

    public Transform grassPlane;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        grassPlane.position = new Vector3(grassPlane.position.x, grassPlane.position.y, grassPlane.position.z + speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);

        if ((int)transform.position.z >= index*60f)
        {
            GenerationManager.instance.GenerateTown(index);
            index++;
        }
        
	}
}
