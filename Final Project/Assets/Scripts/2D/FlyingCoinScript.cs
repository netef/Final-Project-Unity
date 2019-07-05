using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCoinScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * 5 * Time.deltaTime); 
    }
}