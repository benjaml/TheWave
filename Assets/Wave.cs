using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float speed;
    private int index = 0;
    private float height;
    private float heightMax = 100;
    private float charge;
    private float chargeMax = 250;
    public float decreaseSpeed;

    public Transform grassPlane;

	// Use this for initialization
	void Start () {

        height = heightMax * PlayerPrefs.GetFloat( "Ratio" , 0.5f);
        Debug.Log(height +" ratio: "+ PlayerPrefs.GetFloat("Ratio", 0.5f));
        charge = chargeMax / 2;
	}
	
	// Update is called once per frame
	void Update () {
        grassPlane.position = new Vector3(grassPlane.position.x, grassPlane.position.y, grassPlane.position.z + speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, height - 100f , transform.position.z + speed * Time.deltaTime);

        if ((int)transform.position.z >= index*60f)
        {
            GenerationManager.instance.GenerateTown(index);
            index++;
        }
        
        charge = Mathf.Max(0, charge - (charge * 0.1f) * Time.deltaTime);
        if (Input.anyKeyDown)
        {
            charge += 1f;
        }
        float ratio = charge / chargeMax;

        ratio = 1 - Mathf.Max(0, ratio - 0.01f);

        height -= decreaseSpeed * ratio * Time.deltaTime;
    }
}
