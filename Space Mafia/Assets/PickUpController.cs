using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GunScript G1;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForawrdForce, dropUpdwardForce;

    public bool equipped;
    public static bool slotFull;

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable script
        G1.enabled = true;
    }


    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);
        //Make Rigidbody kinematic and BoxCollider a normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Enable script
        G1.enabled = false; 



    }
    // Start is called before the first frame update
    void Start()
    {
        if (!equipped)
        {
            G1.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            G1.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is in rnage and "E" is pressded,
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }
}
