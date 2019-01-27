using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Crash : MonoBehaviour {

    AudioSource Crash;

	// Use this for initialization
	void Start () {
        Crash = GetComponent<AudioSource>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 2)
            Crash.Play();
    }

}
