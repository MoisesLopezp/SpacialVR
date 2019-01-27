using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Valvula : MonoBehaviour {

	public bool isAttach = false;
    public float inc_dec_Valor = 0;
    public scr_Items ItemsScript;
    public GameObject ParentOrigin;
    bool recorrido = false;
    scr_PlayerStats PlayerScript;
    Rigidbody Padre;
    float PosIniX, posIniZ, PosIniY;

    private void Start()
    {
        PlayerScript = scr_Mng.GM.Astronaut;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hand")
        {
            if (ItemsScript.isGrabByHand)
            {
                isAttach = true;
                ItemsScript.MiniGame_BombaAire();
                PosIniX = transform.parent.position.x;
                posIniZ = transform.parent.position.z;
                PosIniY = transform.parent.position.y;             
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
        {
        //    isAttach = false;
        }
    }

    private void Update()
    {
        if (isAttach)
        {
            if(transform.parent.position.x != PosIniX || transform.parent.position.z != posIniZ)
            {
                transform.parent.position = new Vector3(PosIniX, transform.parent.position.y, posIniZ);

               
            }

            if (transform.parent.position.y <= PosIniY )
            {
                transform.parent.position = new Vector3(transform.parent.position.x, PosIniY, transform.parent.position.z);
                recorrido = true;
            }
            if (transform.parent.position.y >= (PosIniY + 0.12f))
            {
                if (recorrido == true)
                {
                    PlayerScript.Add_Air(inc_dec_Valor);
                    recorrido = false;
                }
                transform.parent.position = new Vector3(transform.parent.position.x, PosIniY + 0.12f, transform.parent.position.z);
            }
            
            //ItemsScript.MiniGame_BombaAire();
            
        }
        else
        {
            transform.SetParent(ParentOrigin.gameObject.transform);
        }


    }

}
