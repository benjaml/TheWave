using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour {

    public static GenerationManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        StartGenerateTown();
    }

    public List<GameObject> buildingList;
    public GameObject buildingBlock;
    public Transform wave;

    public void StartGenerateTown()
    {

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Instantiate(buildingBlock, new Vector3(i * -90f, 0f, 240f+j*60f), Quaternion.identity);
            }
            
        }
    }

    public void GenerateTown(int wavePosZ)
    {
        
        for(int i = 0; i < 6; i++)
        {
            Instantiate(buildingBlock, new Vector3(i * -90f, 0f, wavePosZ*60f + 540f), Quaternion.identity);
        }
    }
}
