using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    GameObject mario;
    void Start()
    {
        mario = gameObject.transform.parent.gameObject;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("hit"))
            mario.GetComponent<MarioScript>().isGrounded = true;
        else if (collision.gameObject.CompareTag("enemy"))
        {
            //add enemy death animation
            Destroy(collision.gameObject);
        }
    }
}