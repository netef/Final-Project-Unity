﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour
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

                SceneManager.LoadScene(3, LoadSceneMode.Single);

       
        }
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);

    }
}
