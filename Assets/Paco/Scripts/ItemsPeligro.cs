﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPeligro : MonoBehaviour {
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

    public float damage = 0;
    float magnitud;

    public int tipoObj;
    
    public bool detonante = false;
    bool multiplicador = false;
      
    RaycastHit hit;  
    GameObject player;
    Vector3 miPosCentro;
    scr_PlayerStats PlayerScript;

    // Use this for initialization
    void Start () {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_PlayerStats>();
        player = GameObject.FindGameObjectWithTag("Player");
        
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
                case 1:
                    break;
                case 2:
                    PlayerScript.Add_Dmg(damage);
                    Destroy(this.gameObject);
                    break;
                case 3:
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
        if(other.gameObject.tag == "Player" && tipoObj == 3)
        {
            PlayerScript.Add_Dmg(damage);
            //Debug.Log(PlayerScript.SaludTotal);
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
                    Destroy(this.gameObject);
                    break;
                case 9:
                    PlayerScript.Add_Food(damage);
                    Destroy(this.gameObject);
                    break;
                case 10:
                    PlayerScript.St_Health = 100;
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
