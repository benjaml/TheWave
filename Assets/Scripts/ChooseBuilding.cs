using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuilding : MonoBehaviour {
    
    void Start()
    {
        Destroy(this.transform.root.gameObject, Random.Range(13f,15f));
    }
}
