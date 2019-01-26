using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_RandomChild : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject selected = transform.GetChild(Random.Range(0, transform.childCount)).gameObject;
        selected.SetActive(true);
        selected.transform.parent = null;
        Destroy(gameObject);
    }
	
}
