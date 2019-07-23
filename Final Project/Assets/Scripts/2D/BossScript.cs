using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public GameObject shots, leftWall, rightWall;
    public Transform leftPos, rightPos;
    public int direction;
    public bool facingRight;
    public float jumpPower, speed;
    Rigidbody2D rb;
    bool move;

    void Start()
    {
        InvokeRepeating("Jump", .01f, 2);
        InvokeRepeating("Shot", .01f, 1.5f);
        rb = GetComponent<Rigidbody2D>();
        facingRight = false;
        move = false;
        direction = 1;
    }

    void FixedUpdate()
    {
        if (move)
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }

    void Shot()
    {
        Vector3 location = new Vector3(transform.position.x, transform.position.y - 1.5f, 0);
        var ignore = Instantiate(shots, location, Quaternion.identity);
        ignore.transform.localScale = new Vector3(ignore.transform.localScale.x * direction, ignore.transform.localScale.y, ignore.transform.localScale.z);
        Physics2D.IgnoreCollision(ignore.GetComponent<CapsuleCollider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(ignore.GetComponent<CapsuleCollider2D>(), leftWall.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(ignore.GetComponent<CapsuleCollider2D>(), rightWall.GetComponent<Collider2D>());
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    void Look()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void Side()
    {
        Look();
        direction *= -1;
        rb.velocity = new Vector2(0, rb.velocity.y);
        move = false;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.2f);
        move = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("side"))
            Side();
    }
}