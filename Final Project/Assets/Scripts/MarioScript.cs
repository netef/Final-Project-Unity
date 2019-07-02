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
    public bool grow, shrink, flower, canHit;
    int counter;
    SpriteRenderer renderer;
    Vector2 movement;
    Vector2 rayPosition;
    GameObject mario;


    void Start()
    {

        anim = GetComponent<Animator>();
        isGrounded = true;
        facingRight = true;
        grow = false;
        shrink = false;
        flower = false;
        canHit = true;
        counter = 0;
        renderer = GetComponent<SpriteRenderer>();
        mario = gameObject.transform.parent.gameObject;
        rb = mario.GetComponent<Rigidbody2D>();
        PlayerPrefs.SetInt("powerUp", 0);
    }

    void Update()
    {
        //run - idle transitions
        horizontalSpeed = Input.GetAxis("Horizontal");
        //square is always above 0
        anim.SetFloat("speed", horizontalSpeed * horizontalSpeed);
        //jump animation
        anim.SetBool("Ground", isGrounded);

        //Jumps only if grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * 30, ForceMode2D.Impulse);
            isGrounded = false;
        }


        //Shoots fireballs if mario took a flower
        if (Input.GetKeyDown(KeyCode.A) && PlayerPrefs.GetInt("powerUp", 0) == 2)
            Instantiate(fireBall, transform.position, Quaternion.identity);

        //Makes mario bigger when he takes a mushroom
        if (grow)
        {
            mario.transform.localScale *= 1.01f;
            counter++;
            if (counter == 50)
                grow = false;
        }

        //Makes mario smaller when he gets hit by an enemy
        if (shrink)
        {
            mario.transform.localScale *= 0.99f;
            counter--;
            if (counter == 0)
                shrink = false;
        }

        if (!canHit)
            renderer.enabled = !renderer.enabled;
    }

    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("powerUp", 0) == 0)
            rayPosition = new Vector2(transform.position.x, transform.position.y - 0.8f);
        else
            rayPosition = new Vector2(transform.position.x, transform.position.y - 1.6f);

        RaycastHit2D hit = Physics2D.Raycast(rayPosition, Vector2.down, 0.2f);
        if (hit)
        {
            if (hit.transform.CompareTag("ground") || hit.transform.CompareTag("hit"))
                isGrounded = true;
            else
                isGrounded = false;
        }
        else
        {
            isGrounded = false;
        }
        rb.velocity = new Vector2(horizontalSpeed * speed, rb.velocity.y);
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
        else if (collision.gameObject.CompareTag("enemy") ||
            collision.gameObject.CompareTag("kill") && canHit)
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
            canHit = false;
            StartCoroutine(Wait());
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") ||
            collision.gameObject.CompareTag("kill") && canHit)
        {
            if (PlayerPrefs.GetInt("powerUp", 0) == 0)
            {
                Debug.Log("GameOver");
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
            canHit = false;
            StartCoroutine(Wait());
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        canHit = true;
        renderer.enabled = true;
    }
}