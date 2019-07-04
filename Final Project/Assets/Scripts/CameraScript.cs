using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject mario;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - mario.transform.position;
    }

    void Update()
    {
        transform.position = transform.position = new Vector3(mario.transform.position.x + offset.x, mario.transform.position.y+offset.y, transform.position.z);

    }
}