﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript3D : MonoBehaviour
{
    public GameObject mario;
    private Vector3 offset = new Vector3(3f,0,0);
    void Start()
    {
        offset = transform.position - mario.transform.position;
    }

    void Update()
    {
        transform.position = transform.position = new Vector3(mario.transform.position.x + offset.x, mario.transform.position.y + offset.y, transform.position.z);
    }
}
