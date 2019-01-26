using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Explo : MonoBehaviour {

    public GameObject[] v_Items;
    [Range(20,100)]
    public float v_Fuerza;
    [Range(1, 30),Tooltip("Cuantos objetos a crear")]
    public int v_Cuantos=20;
    [Range(0, 5)]
    public int v_Tipo = 0;
    private void Awake()
    {
        Vector3 rand = Vector3.zero;
        GameObject _obj;
        for(int i=0; i<v_Cuantos; i++)
        {
            _obj = Instantiate(v_Items[v_Tipo], transform.position, Quaternion.identity);
            rand.x = Random.Range(-10,10);
            rand.y = Random.Range(-10,10);
            rand.z = Random.Range(-10,10);
            _obj.GetComponent<Rigidbody>().AddForce(rand* v_Fuerza);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1.0f);
    }
}
