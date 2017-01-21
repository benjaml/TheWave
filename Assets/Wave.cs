using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float speed;
    private int index = 0;
    private float charge;
    private float chargeMax = 100;
    public float decreaseSpeed;

    public Transform grassPlane;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        grassPlane.position = new Vector3(grassPlane.position.x, grassPlane.position.y, grassPlane.position.z + speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, (charge-100), transform.position.z + speed * Time.deltaTime);

        if ((int)transform.position.z >= index*60f)
        {
            GenerationManager.instance.GenerateTown(index);
            index++;
        }

        
        /*if (Input.anyKeyDown)
        {
            charge -= (decreaseSpeed * 0.001f) * Time.deltaTime;
        }else
        {
            charge -= decreaseSpeed * Time.deltaTime;
        }*/

        charge = Mathf.Max(0, charge - (charge * 0.1f) * Time.deltaTime);
        if (Input.anyKeyDown)
        {
            charge += 1f;
        }
        float ratio = charge / chargeMax;
    }
}
