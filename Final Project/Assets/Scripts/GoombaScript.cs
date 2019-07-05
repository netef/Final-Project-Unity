﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScript : MonoBehaviour
{
    int speed;
    bool right;
    bool alive;
    Rigidbody2D rb;
    void Start()
    {
        speed = 3;
        right = false;
        alive = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (alive)
        {
            if (right)
                rb.velocity = new Vector2(speed, rb.velocity.y);
            else
                rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
            right = !right;
    }
}