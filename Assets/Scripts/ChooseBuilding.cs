using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuilding : MonoBehaviour {

    void Start()
    {
        ChooseRandomBuild();
    }

	void ChooseRandomBuild()
    {
        int rand = (int)Random.Range(0, GenerationManager.instance.buildingList.Count);
        Instantiate(GenerationManager.instance.buildingList[rand], transform.position, GenerationManager.instance.buildingList[rand].transform.rotation);
    }
}
