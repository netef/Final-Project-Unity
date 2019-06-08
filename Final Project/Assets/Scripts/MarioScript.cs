using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioScript : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public bool isGrounded;
    bool facingRight;
    Animator anim;
    float horizontalSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isGrounded = true;
        facingRight = true;
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed * Input.GetAxis("Horizontal"));

        //run - idle transitions
        horizontalSpeed = Input.GetAxis("Horizontal");
        //square is always above 0
        anim.SetFloat("speed", horizontalSpeed * horizontalSpeed);
        //jump animation
        anim.SetBool("Ground", isGrounded);


        if (Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, 30), ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            isGrounded = false;
        }
    }
}