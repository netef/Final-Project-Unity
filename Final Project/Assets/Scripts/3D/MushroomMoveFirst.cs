using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMoveFirst : MonoBehaviour
{
    public GameObject actualMushroom, thisMushroom;
    Boolean isFirst = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            this.transform.parent = null;
        if (isFirst)
        {
            transform.Translate(Vector3.up * 2 * Time.deltaTime, Space.World);
            CloseAnim();
            isFirst = false;

        }
        else
        {
            thisMushroom.SetActive(false);
            actualMushroom.SetActive(true);

        }
        
    }
    IEnumerable CloseAnim()
    {
        yield return new WaitForSeconds(110.5f); 
        thisMushroom.SetActive(false);
        actualMushroom.SetActive(true);
    }
}
