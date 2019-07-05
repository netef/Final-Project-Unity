using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockNonDestroy : MonoBehaviour
{
    float XPos, YPos, ZPos;
    // Start is called before the first frame update
    void Start()
    {
        XPos = transform.position.x;
        YPos = transform.position.y;
        ZPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        transform.GetComponent<Collider>().isTrigger = false;
        if(other.gameObject.tag == "Player")
        {
            transform.position = new Vector3(XPos, YPos + 0.2f, ZPos);
            yield return new WaitForSeconds(0.08f);
             transform.position = new Vector3(XPos, YPos, ZPos);
            yield return new WaitForSeconds(0.25f);

            transform.GetComponent<Collider>().isTrigger = true;


        }
    }

}
