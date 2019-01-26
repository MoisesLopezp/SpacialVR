using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_RandomGen : MonoBehaviour {

    public GameObject Trash;
    public GameObject RadioactiveArea;
    public GameObject Explosives;
    public GameObject BlackHole;
    public GameObject AirBomb;
    public GameObject Gifts;
    public GameObject Food;
    public GameObject Communicators;
    public GameObject SpacialSuit;

    public Vector3 MaxDim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject SpawnRandomObject(GameObject obj)
    {
        float rx = Random.Range(-MaxDim.x, MaxDim.x);
        float ry = Random.Range(-MaxDim.y, MaxDim.y);
        float rz = Random.Range(-MaxDim.z, MaxDim.z);
        return Instantiate(obj,new Vector3(rx,ry,rz),Random.rotation);
    }
}
