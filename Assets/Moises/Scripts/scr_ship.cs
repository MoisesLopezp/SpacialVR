﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ship : MonoBehaviour {

    Rigidbody RB;
    public Rigidbody Meteoro;
    public GameObject Init;
    public GameObject Explo;

	// Use this for initialization
	void Start () {
        RB = GetComponent<Rigidbody>();
        RB.AddForce(transform.up * -200f);
        Meteoro.AddForce(Meteoro.transform.up*-150f);
        StartCoroutine(TimeCrash());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator TimeCrash()
    {
        yield return new WaitForSeconds(3f);
        Explo.SetActive(true);
        Destroy(Explo, 3f);
        yield return new WaitForSeconds(1f);
        scr_Mng.GM.TutorialScreen.SetActive(true);
        Init.transform.parent = null;
        Init.SetActive(true);
        Destroy(Meteoro.gameObject, 10f);
        Destroy(gameObject,10f);
        yield return new WaitForSeconds(6f);
        scr_Mng.GM.TutorialScreen.SetActive(false);
    }

}
