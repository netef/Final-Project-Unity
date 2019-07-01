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
        else if (collision.gameObject.CompareTag("enemy") && mario.GetComponent<MarioScript>().isGrounded == false)
        {
            mario.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 40, ForceMode2D.Impulse);
            //add enemy death animation
            //Destroy(collision.gameObject);
        }
    }
}