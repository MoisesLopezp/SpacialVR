using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class C_Itemsss
{
    public GameObject v_Modelo;
    [Range(1, 10)]
    public int v_Cuantos = 1;
}
public class SCR_Explo : MonoBehaviour {

    public List< C_Itemsss> v_Items;

    [Range(1,100)]
    public float v_Fuerza;

    private void Awake()
    {
        Vector3 rand = Vector3.zero;
        GameObject _obj;
        int _cuantos = 0;
        for (int i = 0; i < v_Items.Count; i++)
        {
            _cuantos = v_Items[i].v_Cuantos;
            for(int j=0; j<_cuantos; j++)
            {
                _obj = Instantiate(v_Items[i].v_Modelo, transform.position, Quaternion.identity);
                rand.x = Random.Range(-6, 6);
                rand.y = Random.Range(-6, 6);
                rand.z = Random.Range(-6, 6);
                _obj.GetComponent<Rigidbody>().AddForce(rand * v_Fuerza);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
