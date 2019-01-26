using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Asteroid : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.GetChild(Random.Range(0,5)).gameObject.SetActive(true);
	}
	
}
