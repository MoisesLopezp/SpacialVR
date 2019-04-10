using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SCR_Controller : MonoBehaviour
{
    private Hand hand;

    private void Start()
    {
        hand = GetComponent<Hand>();
    }

    private void Update()
    {
        
    }
}