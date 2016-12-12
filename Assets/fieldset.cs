using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/* This script was developed by Drennen Dooms. All rights reserved. */

public class fieldset : MonoBehaviour
{


    public GameObject NEWOBJ;
    public float dist;
    public Text txt;
    public bool dofEnabled;

    void Awake()
    {
        dofEnabled = true;
        txt = GameObject.Find("DISTANCETEXT").GetComponent<Text>();
    }

    void Update()
    {
        if (dofEnabled)
        {
            NEWOBJ.GetComponent<DepthOfField1>().enabled = true;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
            {
                dist = hit.distance;
                txt.text = hit.distance.ToString("F2");
                var obj = NEWOBJ.GetComponent<DepthOfField1>();
                obj.focalLength = hit.distance;
                obj.focalSize = .1f;

            }

            else
            {
                var obj = Camera.main.GetComponent<DepthOfField1>();
                obj.focalLength = 2f;
                obj.focalSize = .7f;
            }

        }
        else
        {
            NEWOBJ.GetComponent<DepthOfField1>().enabled = false;
        }
        //Physics.Raycast(transform.position, transform.forward, out hit);




    }






}
