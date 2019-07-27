using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (GlobalLives.livesAmount == 0)
        {
            SceneManager.LoadScene(4, LoadSceneMode.Single);

        }
        else
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);


        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GlobalLives.livesAmount -= 1;
        if(GlobalLives.livesAmount <= 0)
        {
            SceneManager.LoadScene(5, LoadSceneMode.Single);
            
        }
        else
        {
            SceneManager.LoadScene(5, LoadSceneMode.Single);
   

        }
    }
}
