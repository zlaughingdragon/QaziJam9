using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent (typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    public float speed, maxSpeed = 15f, speedMod, turnMod, jumpMod;
    public GameObject playerCam;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        speed = rb.velocity.magnitude;
        if(speed < maxSpeed)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                rb.velocity = transform.forward * speedMod * Input.GetAxis("Vertical");
            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                rb.velocity = transform.right * speedMod * Input.GetAxis("Horizontal");
            }
            
        }
        if(Input.GetAxis("Vertical") == 0)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, .8f);
        }




    }





}
