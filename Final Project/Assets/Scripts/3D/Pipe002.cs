using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe002 : MonoBehaviour
{
    bool StoodOn;

    // Start is called before the first frame update
    void Start()
    {
        StoodOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(StoodOn)
        {
            
            GameObject.Find("MaleFreeSimpleMovement1").GetComponent<SimpleCharacterControl>().enabled = true;
            GameObject.Find("MaleFreeSimpleMovement1").transform.position = new Vector3(31.97f, 6.01f, 11.24f);//(47f, 5.4f   , 12.8f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        StoodOn = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        StoodOn = true;
    }
   
}
