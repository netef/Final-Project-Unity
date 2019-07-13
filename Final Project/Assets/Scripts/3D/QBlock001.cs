using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBlock001 : MonoBehaviour
{
   public GameObject qBlock,deadLock,mushroom;
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
        qBlock.SetActive(false);
        deadLock.SetActive(true);
        new WaitForSeconds(0.2f);
        mushroom.SetActive(true);

    }
}
