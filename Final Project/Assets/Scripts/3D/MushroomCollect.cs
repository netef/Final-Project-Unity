using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCollect : MonoBehaviour
{
    public GameObject thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            this.transform.position =new Vector3(0, -1000, 0);
            thePlayer.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
        }
    }
}
