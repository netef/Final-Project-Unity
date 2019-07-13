using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeScroll : MonoBehaviour
{
    float resetPoint,currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        resetPoint = -20f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= resetPoint)
        {
            transform.position = new Vector3(transform.position.x, -25.0f, transform.position.z);

        }
        transform.Translate(Vector3.up * Time.deltaTime);

        }
}

