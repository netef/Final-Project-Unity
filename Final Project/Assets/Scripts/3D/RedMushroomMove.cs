using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMushroomMove : MonoBehaviour
{
    float left = -20f, right = 11f;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.parent = null;
        if (direction == 1)
        {
            transform.Translate(Vector3.right * 2 * Time.deltaTime, Space.World);
            direction = 1;
        }
        if (transform.position.x > right)
        {
            direction = 2;
        }
        if (direction == 2)
        {
            transform.Translate(Vector3.right * -2 * Time.deltaTime, Space.World);
            direction = 2;
        }
        if (transform.position.x < left)
        {
            direction = 1;
        }
    }
}
