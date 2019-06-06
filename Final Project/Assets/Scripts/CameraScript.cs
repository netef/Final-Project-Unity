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
        transform.position = mario.transform.position + offset;
    }
}