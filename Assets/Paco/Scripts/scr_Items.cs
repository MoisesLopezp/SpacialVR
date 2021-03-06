﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float inc_dec_Valor = 0;
    float magnitud;


    public int tipoObj;
   
    public bool detonante = false; // para que explote y haga daño se necesita cambiar el bool a true
    bool multiplicador = false;
    bool hoyonegro = false;
    public bool isGrabByHand = false;

    //GameObject ui_Image;
    public GameObject prefab_Canvas;
   

    RaycastHit hit;    
    GameObject player, Canvas, posAttach;
    Transform Jugador;
    Vector3 miPosCentro;
    Rigidbody mirigi, jugarigi;
    scr_PlayerStats PlayerScript;

    Comunicadores ComunScript;

    // Use this for initialization
    void Start () {
        /*
        if (tipoObj != 3 && tipoObj != 5 && tipoObj != 1)
        {
            ui_Image = Instantiate(prefab_Canvas, this.gameObject.transform);
            ui_Image.transform.SetParent(this.gameObject.transform);
           
        }
        */
        
        mirigi = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        posAttach = GameObject.FindGameObjectWithTag("Hand");
        ComunScript = scr_Mng.GM.Comunicador;
        Jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PlayerScript = scr_Mng.GM.Astronaut;
        Vector3 Direccion = Vector3.zero;
        
        switch (tipoObj)
        {
            case 1://es meteoro
                Direccion = (Jugador.transform.position - this.transform.position).normalized;
                Direccion = new Vector3(Direccion.x + Random.Range(-0.1f, 0.1f), Direccion.y + Random.Range(-0.1f, 0.1f), Direccion.z + Random.Range(-0.1f, 0.1f));
                mirigi.AddForce(Direccion * fuerzaini, ForceMode.Impulse);
                break;
            case 5://hoyonegro
                hoyonegro = !hoyonegro;
                jugarigi = Jugador.GetComponent<Rigidbody>();
                break;
            case 11:
                Direccion = (Jugador.transform.position - this.transform.position).normalized;
                Direccion = new Vector3(Direccion.x + Random.Range(-0.1f, 0.1f), Direccion.y + Random.Range(-0.1f, 0.1f), Direccion.z + Random.Range(-0.1f, 0.1f));
                mirigi.AddForce(Direccion * fuerzaini * 0.25f, ForceMode.Impulse);
                break;
            default:
                break;
        }

    }
	
	// Update is called once per frame
	void Update () {
        /*
        if (tipoObj != 3 && tipoObj != 5 && tipoObj != 1)
        {
            ui_Image.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z);
            ui_Image.transform.GetChild(0).GetChild(0).transform.LookAt(Camera.main.transform);
        }
        */
        if (tipoObj == 4)
        {
            Dinamita();
        }

        /*if(tipoObj == 6 && isAttach)
        {
            MiniGame_BombaAire();
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
       

        if (tipoObj == 4)
        {
            detonante = true;
        }
        
        if (collision.gameObject.tag == "Player" )
        {
            
            switch (tipoObj)
            {
                case 1:
                    PlayerScript.Add_Dmg(inc_dec_Valor);
                    ComunScript.RecibiDanio = true;
                    Destroy(this.gameObject);
                    break;
                case 2:
                    PlayerScript.Add_Dmg(inc_dec_Valor);
                    ComunScript.RecibiDanio = true;
                    break;
               
                case 4:
                    inc_dec_Valor -= magnitud;                   
                    PlayerScript.Add_Dmg(inc_dec_Valor);
                    ComunScript.RecibiDanio = true;
                    GameObject expl = Instantiate(scr_Mng.GM.Explosion, transform.position, Quaternion.identity);
                    Destroy(expl, 3f);
                    Destroy(gameObject);
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
                    PlayerScript.Add_Dmg(inc_dec_Valor*Time.deltaTime);
                    ComunScript.RecibiDanio = true;
                    break;
                case 5:
                    Vector3 nuevadireccion = (this.transform.position - Jugador.transform.position).normalized;
                    float distance = Vector3.Distance(this.transform.position, Jugador.transform.position);
                    if (distance < 0.2f) { distance = 0.2f; }
                    float force = (fuerzaini*0.1f) / (distance * distance);
                    jugarigi.AddForce(nuevadireccion * force);
                    if (jugarigi.velocity.sqrMagnitude > 1.5f)
                    {
                        jugarigi.velocity *= 0.99f;
                    }
                    ComunScript.RecibiDanio = true;
                    PlayerScript.Add_Dmg(1f * Time.deltaTime);
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
                    {
                        PlayerScript.Add_Happiness(inc_dec_Valor);
                        //ComunScript.Play_Audio();
                        Destroy(this.gameObject);
                    }break;
                case 9:
                    {
                        PlayerScript.Add_Food(inc_dec_Valor);
                        //ComunScript.Play_Audio();
                        Destroy(this.gameObject);
                    }break;
                case 10:
                    {
                        PlayerScript.St_Health = 100;
                        //ComunScript.Play_Audio();
                        Destroy(this.gameObject);
                    }
                    break;
                case 11:
                    {
                        ComunScript.RecibiDanio = false;
                        ComunScript.Play_Audio();
                        Destroy(this.gameObject);
                    }
                    break;
                case 6:
                    {
                        PlayerScript.Add_Air(inc_dec_Valor);
                        Destroy(this.gameObject);
                    }
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
                rb.AddExplosionForce(2f, transform.position, 0.5f);
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
    
    public void MiniGame_BombaAire()
    {
        float maxPosY = 0.15f, minPosY = -0.03f;
        //transform.position = posAttach;
        // Debug.Log(posAttach.transform.position.y + " " + transform.GetChild(1).localPosition.y);
        //transform.GetChild(1).position = new Vector3(transform.GetChild(1).position.x, posAttach.transform.position.y , transform.GetChild(1).position.z);
        transform.GetChild(1).SetParent(posAttach.gameObject.transform);


      /* if(transform.GetChild(1).localPosition.y >= maxPosY)
        {
            transform.GetChild(1).localPosition = new Vector3(transform.GetChild(1).localPosition.x,maxPosY, transform.GetChild(1).localPosition.z);
            //PlayerScript.Add_Air(inc_dec_Valor);
        }

        if (transform.GetChild(1).localPosition.y <= minPosY)
        {
            transform.GetChild(1).position = new Vector3(transform.GetChild(1).localPosition.x, minPosY,  transform.GetChild(1).localPosition.z);
        }*/


    }

}
