using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroides : MonoBehaviour {

    public int tipoobjeto, dmg = 10;
    public float fuerzaini = 1.0f;
    bool hoyonegro = false;
    Rigidbody mirigi, jugarigi;
    Transform Jugador;
    // Use this for initialization

    void Start () {
        mirigi = GetComponent<Rigidbody>();
        Jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        switch (tipoobjeto)
        {
            case 1://es meteoro
                Vector3 Direccion = (Jugador.transform.position - this.transform.position).normalized;
                Direccion = new Vector3(Direccion.x + Random.Range(-0.1f, 0.1f), Direccion.y + Random.Range(-0.1f, 0.1f), Direccion.z + Random.Range(-0.1f, 0.1f));
                mirigi.AddForce(Direccion * fuerzaini, ForceMode.Impulse);
                break;
            case 2://hoyonegro
                hoyonegro = !hoyonegro;
                jugarigi = Jugador.GetComponentInChildren<Rigidbody>();
                break;
            default:
                break;
        }
	}


    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Vector3 nuevadireccion = (this.transform.position - Jugador.transform.position).normalized;
            float distance = Vector3.Distance(this.transform.position, Jugador.transform.position);
            if (distance < 0.2f){distance = 0.2f;}
            float force = fuerzaini / (distance * distance);
            jugarigi.AddForce(nuevadireccion*force);
        }
    }

}
