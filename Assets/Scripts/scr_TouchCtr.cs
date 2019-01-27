using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_TouchCtr : MonoBehaviour {

    public OVRInput.Controller MyController;
    public Animator AnimatorHand;
    public GameObject Player;
    public GameObject Camera;

    LineRenderer Selector;

    GameObject Hand;

    [HideInInspector]
    public GameObject ObjectGrab;

    [HideInInspector]
    public GameObject PosibleObjectGrab;

    OVRInput.Axis1D GrabButton;
    OVRInput.Axis1D AcctionButton;

    bool IsRight = true;

    Rigidbody PlayerRB;

    Rigidbody RB;

    public LayerMask UILayer;

    // Use this for initialization
    void Start () {
        ObjectGrab = null;
        PosibleObjectGrab = null;
        PlayerRB = Player.GetComponent<Rigidbody>();
        RB = GetComponent<Rigidbody>();

        Hand = AnimatorHand.gameObject;
        if (MyController == OVRInput.Controller.RTouch)
        {
            Selector = GetComponent<LineRenderer>();
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

        transform.localPosition = OVRInput.GetLocalControllerPosition(MyController);
        //transform.position = Vector3.MoveTowards(transform.position, OVRInput.GetLocalControllerPosition(MyController), 0.03f);
        transform.rotation = OVRInput.GetLocalControllerRotation(MyController);
            //Quaternion.Euler(HandQ.eulerAngles.x, HandQ.eulerAngles.y, HandQ.eulerAngles.z);

        if (IsRight)
        {
            Selector.SetPosition(0, transform.position);
            Selector.SetPosition(1, transform.forward * 10);
            if (OVRInput.Get(AcctionButton) > 0.5)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, transform.forward, out hit, 15f, UILayer);
                if (hit.transform.CompareTag("Button"))
                {
                    Debug.Log("HitButton");
                }
            }
                
        }

        if (OVRInput.Get(GrabButton)>0.5)
        {
            AnimatorHand.SetBool("Grab",true);
            if (PosibleObjectGrab && !ObjectGrab)
            {
                ObjectGrab = PosibleObjectGrab;
                ObjectGrab.transform.position = Hand.transform.position;
                ObjectGrab.transform.rotation = Hand.transform.rotation;
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
        
        if (ObjectGrab)
        {
            ObjectGrab.transform.position = Hand.transform.position;
            ObjectGrab.transform.rotation = Hand.transform.rotation;
            ObjectGrab.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            PlayerRB.AddForce(Player.transform.up*8f);
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            PlayerRB.AddForce(Player.transform.up*-8f);
        }

        if (Input.GetAxis("Vertical")>0.5f)
        {
            PlayerRB.AddForce(Camera.transform.forward);
        }

        if (Input.GetAxis("Vertical") < -0.5f)
        {
            PlayerRB.AddForce(Camera.transform.forward*-1f);
        }

        if (Input.GetAxis("Horizontal") > 0.5f)
        {
            PlayerRB.AddForce(Camera.transform.right);
        }

        if (Input.GetAxis("Horizontal") < -0.5f)
        {
            PlayerRB.AddForce(Camera.transform.right*-1f);
        }

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
