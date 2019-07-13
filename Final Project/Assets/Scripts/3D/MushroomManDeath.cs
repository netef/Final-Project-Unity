using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManDeath : MonoBehaviour
{
     public GameObject mushroomMan;
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
        Destroy(GameObject.Find("MushroomMan001").gameObject);
        if(mushroomMan!=null)
        Destroy(mushroomMan);
    }
}
