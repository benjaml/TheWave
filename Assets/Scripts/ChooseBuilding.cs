using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuilding : MonoBehaviour {
    
    void Start()
    {
        Destroy(this.transform.root.gameObject, Random.Range(12f,15f));
    }
}
