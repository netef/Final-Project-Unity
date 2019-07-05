using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeyTurtleScript : MonoBehaviour
{
    int counter, speed;
    bool right, once, isRunning;
    Animator anim;
    Rigidbody2D rb;
    void Start()
    {
        counter = 0;
        right = true;
        once = true;
        isRunning = true;
        speed = 3;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        anim.SetBool("run", isRunning);

        if (right)
        {
            if (counter < 240)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                counter++;
            }
            else if (once)
            {
                once = false;
                isRunning = !isRunning;
                StartCoroutine(Wait());
            }
        }
        else if (!right)
        {
            if (counter > 0)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                counter--;
            }
            else if (once)
            {
                once = false;
                isRunning = !isRunning;
                StartCoroutine(Wait());
            }
        }  
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
        isRunning = !isRunning;
        once = true;
        right = !right;
    }
}