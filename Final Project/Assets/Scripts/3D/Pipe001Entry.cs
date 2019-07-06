using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe001Entry : MonoBehaviour
{
    public GameObject pipeEntry;
    bool StoodOn;
    
    void Start()
    {
        StoodOn = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("GoDown"))
        {
            if (StoodOn)
            {
                       transform.position = new Vector3(0, -1000, 0);
                      StartCoroutine(WaitForPipe());                    
            }
        }
    }

    IEnumerator WaitForPipe()
    {
          pipeEntry.GetComponent<Animator>().enabled = true;
          yield return new WaitForSeconds(1);
          pipeEntry.GetComponent<Animator>().enabled = false;
          GameObject.Find("MaleFreeSimpleMovement1").GetComponent<SimpleCharacterControl>().enabled = true;
        GameObject.Find("MaleFreeSimpleMovement1").transform.position = new Vector3(10, -18, 11.24f);
    }

    private void OnTriggerEnter(Collider other)
    {
        StoodOn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        StoodOn = false;
    }
}