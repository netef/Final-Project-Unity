using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    bool goUp;

    void Start()
    {
        goUp = true;
        StartCoroutine(Wait());
    }

    void Update()
    {
        if(goUp)
            transform.Translate(Vector3.up * 3 * Time.deltaTime);
        else
            transform.Translate(Vector3.right * 3 * Time.deltaTime);      
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 9.8f;
        goUp = false;
    }
}