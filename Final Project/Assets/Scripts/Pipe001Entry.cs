using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe001Entry : MonoBehaviour
{
    GameObject pipeEntry;
    int StoodOn=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("GoDown"))
        {
            if (StoodOn == 1)
            {
          /*      GameObject.Find("ThirdPersonController").GetComponent("ThirdPersonUserControl") = false;
                transform.position = new Vector3(0, -1000, 0);
                WaitForPipe();*/
            }
        }
    }

    private void WaitForPipe()
    {
      /*  pipeEntry.GetComponent("Animator").enabled = true;
        yield return new WaitForSeconds(2);
        pipeEntry.GetComponent("Animator").enabled = false;*/
      //  GameObject.Find("FPSController").GetComponent("FirstPersonController").enabled = tr;

    }

    private void OnTriggerExit(Collider other)
    {
        StoodOn = 0;
    }
}
