using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour {
    public float Salud = 100, SaludTotal;
	// Use this for initialization
	void Start () {
		SaludTotal = Salud;
	}
	
	// Update is called once per frame
	void Update () {
		if(SaludTotal <= 0)
        {
            SaludTotal = 0;
        }
	}

    public void JugadorItem(float _stat)
    {
        SaludTotal += _stat;
    }
}
