using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Wave : MonoBehaviour {

    public float speed;
    private int index = 0;
    private float height;
    private float heightMax = 100;
    private float charge;
    private float chargeMax = 250;
    public float decreaseSpeed;
    private bool endGame;

    public Transform grassPlane;
    public Text mashText;

	// Use this for initialization
	void Start () {
        endGame = false;
        height = heightMax * PlayerPrefs.GetFloat( "Ratio" , 0.5f);
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
        
        Camera.main.transform.DOShakePosition(0.5f, 0.15f).OnComplete(() => Camera.main.transform.transform.DOKill());
        mashText.transform.DOShakePosition(0.5f, 3f).OnComplete(() => mashText.transform.DOKill());
        

        charge = Mathf.Max(0, charge - (charge * 0.1f) * Time.deltaTime);
        if (Input.anyKeyDown)
        {
            charge += 1f;
        }

        float ratio = charge / chargeMax;

        ratio = 1 - Mathf.Max(0, ratio - 0.01f);

        height -= decreaseSpeed * ratio * Time.deltaTime;
        
        if (height - 100f <= -100 && !endGame)
        {
            EndOfGame();
        }
        
    }
    
    void EndOfGame()
    {
        endGame = true;
        //TODO changer la targetScene
        ScoreManager.instance.EndOfGame();
        Fade.instance.FadeOut("EndScreen");
    }
}
