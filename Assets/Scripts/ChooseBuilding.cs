using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuilding : MonoBehaviour {

    public Transform wave;

    void Start()
    {
        wave = GenerationManager.instance.wave;
        ChooseRandomBuild();
        Destroy(this.transform.root.gameObject, 10);
    }

	void ChooseRandomBuild()
    {
        int rand = (int)Random.Range(0, GenerationManager.instance.buildingList.Count);
        Instantiate(GenerationManager.instance.buildingList[rand], transform.position, GenerationManager.instance.buildingList[rand].transform.rotation);
    }
}
