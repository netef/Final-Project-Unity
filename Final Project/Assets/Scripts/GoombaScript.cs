using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScript : MonoBehaviour
{
    int speed;
    bool right;
    Rigidbody2D rb;
    void Start()
    {
        speed = 3;
        right = false;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (right)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
            right = !right;
    }
}