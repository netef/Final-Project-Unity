using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomScript : MonoBehaviour
{
    public GameObject enemies;
    bool canEnter;
    void Start()
    {
        canEnter = false;
    }

    void Update()
    {
        if (enemies.transform.childCount == 0)
            canEnter = true;
    }
}