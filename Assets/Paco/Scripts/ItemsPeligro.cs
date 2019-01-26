using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPeligro : MonoBehaviour {

    public int damage = 0;
    public int tipoObj;
    StatsPlayer PlayerScript;

    
	// Use this for initialization
	void Start () {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerScript.JugadorItem(damage);
            Destroy(this.gameObject);
        }
    }
}
