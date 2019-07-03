using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioScriptNew : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public float jumpPower;
    public float velocity;
    public float killPower;
    Animator anim;
    Rigidbody2D rb;
    float direction;
    bool facingRight;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        direction = 0;
        facingRight = true;
    }

    void Update()
    {
        //sets ground animation
        anim.SetBool("Ground", IsGrounded());

        //saves the direction of the input horizontally
        direction = Input.GetAxisRaw("Horizontal");

        //changes the direction mario is looking
        if (direction != 0)
            Look();
    }

    private void FixedUpdate()
    {
        //makes mario jump if he can
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //moves mario
        rb.velocity = new Vector2(velocity * direction, rb.velocity.y);
    }

    //sends a raycast to check if mario is on the ground
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float radius = 1;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.CircleCast(position, radius, direction, distance, groundLayer);
        if (hit.collider != null)
            return true;
        return false;
    }

    //adds force on y axis
    void Jump()
    {
        if (!IsGrounded())
            return;
        else
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    void Look()
    {
        if (direction < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (direction > 0 && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    bool IsEnemy()
    {
        Vector2 position = transform.position - new Vector3(0, 1, 0);
        Vector2 direction = Vector2.down;
        float radius = 0.7f;
        float distance = 2.0f;

        RaycastHit2D hit = Physics2D.CircleCast(position, radius, direction, distance, enemyLayer);
        if (hit.collider != null)
            return true;
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsEnemy())
        {
            Debug.Log("enter");
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            rb.AddForce(Vector2.up * killPower/3, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * killPower, ForceMode2D.Impulse);
            Destroy(collision.gameObject, 4);
        }
    }
}