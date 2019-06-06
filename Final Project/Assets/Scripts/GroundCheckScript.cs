using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    public GameObject mario;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            mario.GetComponent<MarioScript>().isGrounded = true;
            mario.GetComponent<MarioScript>().anim.SetTrigger("grounded");
        }     
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            mario.GetComponent<MarioScript>().isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            mario.GetComponent<MarioScript>().isGrounded = false;
            mario.GetComponent<MarioScript>().anim.ResetTrigger("grounded");
            mario.GetComponent<MarioScript>().anim.SetTrigger("jump");
        }
    }
}