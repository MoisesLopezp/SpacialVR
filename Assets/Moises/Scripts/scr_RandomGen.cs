using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_RandomGen : MonoBehaviour {

    public Vector3 MaxDim;

    public GameObject SpawnRandomObject(GameObject obj)
    {
        float rx = Random.Range(-MaxDim.x, MaxDim.x);
        float ry = Random.Range(-MaxDim.y, MaxDim.y);
        float rz = Random.Range(-MaxDim.z, MaxDim.z);
        return Instantiate(obj,new Vector3(rx,ry,rz),Random.rotation);
    }
}
