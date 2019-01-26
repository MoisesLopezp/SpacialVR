using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_TouchCtr : MonoBehaviour {

    public OVRInput.Controller MyController;
    public Animator AnimatorHand;
    public GameObject Player;

    GameObject Hand;

    [HideInInspector]
    public GameObject ObjectGrab;

    [HideInInspector]
    public GameObject PosibleObjectGrab;

    OVRInput.Axis1D GrabButton;
    OVRInput.Axis1D AcctionButton;

    bool IsRight = true;

	// Use this for initialization
	void Start () {
        ObjectGrab = null;
        PosibleObjectGrab = null;

        Hand = AnimatorHand.gameObject;
        if (MyController == OVRInput.Controller.RTouch)
        {
            IsRight = true;
            GrabButton = OVRInput.Axis1D.SecondaryHandTrigger;
            AcctionButton = OVRInput.Axis1D.SecondaryIndexTrigger;
        } else
        {
            IsRight = false;
            GrabButton = OVRInput.Axis1D.PrimaryHandTrigger;
            AcctionButton = OVRInput.Axis1D.PrimaryIndexTrigger;
        }
    }

    // Update is called once per frame
    void Update () {
        if (!OVRInput.GetControllerPositionTracked(MyController))
            Debug.Log("No traking");
        
        Quaternion HandQ = OVRInput.GetLocalControllerRotation(MyController);

        transform.localPosition = OVRInput.GetLocalControllerPosition(MyController)+new Vector3(0f,0.6f,0f);
        transform.rotation = OVRInput.GetLocalControllerRotation(MyController);
            //Quaternion.Euler(HandQ.eulerAngles.x, HandQ.eulerAngles.y, HandQ.eulerAngles.z);

        if (OVRInput.Get(GrabButton)>0.5)
        {
            AnimatorHand.SetBool("Grab",true);
            if (PosibleObjectGrab && !ObjectGrab)
            {
                ObjectGrab = PosibleObjectGrab;
                ObjectGrab.transform.position = Hand.transform.position;
                ObjectGrab.transform.rotation = Hand.transform.rotation;
                ObjectGrab.transform.parent = transform;
                ObjectGrab.GetComponent<Rigidbody>().useGravity = false;
                ObjectGrab.transform.SetParent(Hand.transform);
                if (ObjectGrab.GetComponent<SphereCollider>())
                    ObjectGrab.GetComponent<SphereCollider>().enabled = false;
                if (ObjectGrab.GetComponent<BoxCollider>())
                    ObjectGrab.GetComponent<BoxCollider>().enabled = false;
            }
        } else
        {
            AnimatorHand.SetBool("Grab", false);
            if (ObjectGrab)
            {
                ObjectGrab.GetComponent<Rigidbody>().useGravity = true;
                ObjectGrab.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(MyController);
                ObjectGrab.transform.parent = null;
                if (ObjectGrab.GetComponent<SphereCollider>())
                    ObjectGrab.GetComponent<SphereCollider>().enabled = true;
                if (ObjectGrab.GetComponent<BoxCollider>())
                    ObjectGrab.GetComponent<BoxCollider>().enabled = true;
                ObjectGrab = null;
            }
        }
        /*
        if (ObjectGrab)
        {
            ObjectGrab.transform.position = Hand.transform.position;
            ObjectGrab.transform.rotation = Hand.transform.rotation;
            if (OVRInput.Get(AcctionButton)>0.5)
            {
                ObjectGrab.SendMessage("Touch_Acction");
            }
        }
        */
        if (!PosibleObjectGrab)
            AnimatorHand.SetBool("Interact", false);
        else
            AnimatorHand.SetBool("Interact", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CanGrab") && !ObjectGrab && !PosibleObjectGrab)
        {
            PosibleObjectGrab = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CanGrab") && PosibleObjectGrab==other.gameObject)
        {
            PosibleObjectGrab = null;
        }
    }
}
