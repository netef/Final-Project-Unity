using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    int counter;
    float speed;
    MarioScriptNew mario;
    Rigidbody2D rb;
    int direction;
    void Start()
    {
        counter = 3;
        speed = 25f;
        mario = GameObject.Find("Mario").GetComponent<MarioScriptNew>();
        rb = GetComponent<Rigidbody2D>();
        if (mario.facingRight)
            direction = 1;
        else
            direction = -1;
    }
    void Update()
    {
        if (counter == 0)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            counter--;
        else if (collision.gameObject.CompareTag("fireball"))
            return;
        else if (collision.gameObject.CompareTag("enemy"))
            Enemy(collision);
        else
            Destroy(gameObject);
    }

    void Enemy(Collision2D collision)
    {
        collision.gameObject.GetComponent<Collider2D>().enabled = false;
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * mario.killPower, ForceMode2D.Impulse);
        collision.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
        Destroy(collision.gameObject, 4);
    }
}