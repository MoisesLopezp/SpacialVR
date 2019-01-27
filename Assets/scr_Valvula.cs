using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Valvula : MonoBehaviour {

	public bool isAttach = false;
    public float inc_dec_Valor = 0;
    public scr_Items ItemsScript;
    public GameObject ParentOrigin;
    scr_PlayerStats PlayerScript;
    float PosIni = 0.12f;

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
                //PosIni += transform.parent.position.y;
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
            StartCoroutine(Oxigeno());
            //ItemsScript.MiniGame_BombaAire();
            
        }
        else
        {
            transform.SetParent(ParentOrigin.gameObject.transform);
        }
    }

    IEnumerator Oxigeno() {
           yield return new WaitForSeconds(1f);
             PlayerScript.Add_Air(inc_dec_Valor);
    }
}
