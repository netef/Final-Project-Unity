using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioScript : MonoBehaviour
{
    public float speed;
    public GameObject fireBall;
    Rigidbody2D rb;
    public bool isGrounded;
    bool facingRight;
    Animator anim;
    float horizontalSpeed;
    bool grow;
    bool shrink;
    bool flower;
    int counter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isGrounded = true;
        facingRight = true;
        grow = false;
        shrink = false;
        flower = false;
        counter = 0;
        PlayerPrefs.SetInt("powerUp", 0);
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

        //Moves the player and handles looking direction
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

        //Jumps only if grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, 30), ForceMode2D.Impulse);
            isGrounded = false;
        }

        //Shoots fireballs if mario took a flower
        if (Input.GetKeyDown(KeyCode.A) && PlayerPrefs.GetInt("powerUp", 0) == 2)
            Instantiate(fireBall, transform.position, Quaternion.identity);

        //Makes mario bigger when he takes a mushroom
        if (grow)
        {
            transform.localScale *= 1.01f;
            counter++;
            if (counter == 50)
                grow = false;
        }

        //Makes mario smaller when he gets hit by an enemy
        if (shrink)
        {
            transform.localScale *= 0.99f;
            counter--;
            if (counter == 0)
                shrink = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("mushroom"))
        {
            if (PlayerPrefs.GetInt("powerUp", 0) == 0)
            {
                grow = true;
                PlayerPrefs.SetInt("powerUp", 1);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("flower"))
        {
            if (PlayerPrefs.GetInt("powerUp", 0) == 0)
                grow = true;

            GetComponent<SpriteRenderer>().color = Color.red;
            PlayerPrefs.SetInt("powerUp", 2);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("enemy"))
        {
            if (PlayerPrefs.GetInt("powerUp", 0) == 0)
            {
                //GameOver();
            }
            else if (PlayerPrefs.GetInt("powerUp", 0) == 1)
            {
                shrink = true;
                PlayerPrefs.SetInt("powerUp", 0);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                PlayerPrefs.SetInt("powerUp", 1);
            }
        }
    }
}