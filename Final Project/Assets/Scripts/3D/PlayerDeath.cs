using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GlobalLives.livesAmount -= 1;
        Application.LoadLevel(0);
        Destroy(GameObject.Find("MaleFreeSimpleMovement1"));
    }
    private void OnTriggerEnter(Collider other)
    {
        GlobalLives.livesAmount -= 1;
        Application.LoadLevel(0);
        Destroy(GameObject.Find("MaleFreeSimpleMovement1"));
    }
}
