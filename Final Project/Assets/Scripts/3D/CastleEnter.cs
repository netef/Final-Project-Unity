using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEnter : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject mainCamera;
    public GameObject secondCamera;
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

        
        mainCamera.SetActive(false);
        secondCamera.SetActive(true);
        GameObject.Find("MaleFreeSimpleMovement1").transform.position = new Vector3(60, -18, 11.24f);

    }
}
