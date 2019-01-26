using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour {
    public int Salud = 100, SaludTotal;
	// Use this for initialization
	void Start () {
		SaludTotal = Salud;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void JugadorItem(int _stat)
    {
        SaludTotal += _stat;
    }
}
