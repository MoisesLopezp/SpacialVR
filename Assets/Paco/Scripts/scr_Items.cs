﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Items : MonoBehaviour {
    /*
    TipoObj
    --Peligro
    1 = Asteroide
    2 = Escombro
    3 = Area radeactiva
    4 = Explosivo
    5 = Hoyo Negro
    --Item
    6 = mini juego
    7 = recuerdo
    9 = alimento
    10 = traje especial
    11 = comunicadores
    */

    public float fuerzaini = 1.0f;
    public float damage = 0;
    float magnitud;


    public int tipoObj;
    public int tipoobjeto, dmg = 10;

    public bool detonante = false; // para que explote y haga daño se necesita cambiar el bool a true
    bool multiplicador = false;
    bool hoyonegro = false;

    RaycastHit hit;
    Transform Jugador;
    GameObject player;
    Vector3 miPosCentro;
    Rigidbody mirigi, jugarigi;
    scr_PlayerStats PlayerScript;
    Comunicadores ComunScript;

    // Use this for initialization
    void Start () {
        mirigi = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        ComunScript = GameObject.Find("Comunicadores").GetComponent<Comunicadores>();
        Jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_PlayerStats>();            
        
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
	
	// Update is called once per frame
	void Update () {
        

        if(tipoObj == 4)
        {
            Dinamita();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" )
        {
            switch (tipoObj)
            {
                
                case 2:
                    PlayerScript.Add_Dmg(damage);
                    Destroy(this.gameObject);
                    break;
               
                case 4:
                    damage -= magnitud;                   
                    PlayerScript.Add_Dmg(damage);
                    break;
                default:
                    break;
            }       
                       
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" )
        {
            switch (tipoObj)
            {
                case 3:
                    PlayerScript.Add_Dmg(damage);                    
                    break;
                case 5:
                    Vector3 nuevadireccion = (this.transform.position - Jugador.transform.position).normalized;
                    float distance = Vector3.Distance(this.transform.position, Jugador.transform.position);
                    if (distance < 0.2f) { distance = 0.2f; }
                    float force = fuerzaini / (distance * distance);
                    jugarigi.AddForce(nuevadireccion * force);
                    break;                
                default:
                    break;
            }

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (tipoObj)
            {
                case 7:
                    PlayerScript.Add_Happiness(damage);
                    ComunScript.Play_Audio();
                    Destroy(this.gameObject);
                    break;
                case 9:
                    PlayerScript.Add_Food(damage);
                    ComunScript.Play_Audio();
                    Destroy(this.gameObject);
                    break;
                case 10:
                    PlayerScript.St_Health = 100;
                    ComunScript.Play_Audio();
                    Destroy(this.gameObject);
                    break;

                default:
                    break;
            }

        }
    }

    void Dinamita()
    {
        Collider[] objEspacio = Physics.OverlapSphere(transform.position, 5f);
        gameObject.transform.LookAt(player.transform);

        foreach (Collider objetos in objEspacio)
        {
            Rigidbody rb = objetos.GetComponent<Rigidbody>();
            if(rb != null && detonante == true)
            {
                rb.AddExplosionForce(50f, transform.position, 5f);
                if (multiplicador == false)
                {
                    this.GetComponent<SphereCollider>().radius *= 12f;
                    multiplicador = true;
                }
            }
        }
        
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Vector3 dirActual = (this.transform.position - player.transform.position);
                magnitud = dirActual.magnitude;            

               
                //Debug.DrawLine(transform.position, transform.forward, Color.red); print("Hit");
            }
        }
    }
}