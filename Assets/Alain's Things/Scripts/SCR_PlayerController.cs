//Codiguito creado por Ronysaurus
using UnityEngine;

//Rny: Se necesita el rigidbody para las fisicas
[RequireComponent(typeof(Rigidbody))]
public class SCR_PlayerController : MonoBehaviour
{
    //Rny: fuerza con la que salta (Al parecer no se necesita) 
    private readonly float jumpForze = 10.0f;
    private Rigidbody rb;

	void Start ()
    {
        //Rny: Se asigna el rigidbody
        rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        //Rny: para saltar (al parecer tampoco se necesita)
        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(Vector3.up * jumpForze);
	}
}
