using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioScriptNew : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public LayerMask questionLayer;
    public GameObject coin;
    public GameObject mushroom;
    public GameObject flower;
    public Sprite sprite;
    public AudioClip powerUpSound;
    public AudioClip JumpSound;
    public AudioClip pipeSound;
    public float jumpPower;
    public float velocity;
    public float killPower;
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer renderer;
    float direction;
    bool facingRight, wasHit;
    AudioSource audio;
    //BoxCollider2D collider;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        //collider = GetComponent<BoxCollider2D>();
        direction = 0;
        facingRight = true;
        wasHit = false;
        PlayerPrefs.SetInt("powerUp", 0);
    }

    void Update()
    {
        //saves the direction of the input horizontally
        direction = Input.GetAxisRaw("Horizontal");

        //sets ground animation
        anim.SetBool("Ground", IsGrounded());
        //changes running and idle animations
        anim.SetFloat("speed", direction * direction);

        //changes the direction mario is looking
        if (direction != 0)
            Look();

        if (wasHit)
            renderer.enabled = !renderer.enabled;

        //makes mario jump if he can
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

    }

    private void FixedUpdate()
    {
        //moves mario
        rb.velocity = new Vector2(velocity * direction, rb.velocity.y);
    }

    //sends a raycast to check if mario is on the ground
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float radius = 0.5f;
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
        {
            audio.PlayOneShot(JumpSound);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    //sets look position
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

    //tells if the target is an enemy or not
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

    bool IsQuestion()
    {
        Vector2 position = transform.position + new Vector3(0, 1, 0);
        Vector2 direction = Vector2.up;
        float distance = 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance);
        if (hit.collider != null)
            if (hit.transform.gameObject.CompareTag("question"))
                return true;
        return false;
    }

    void Enemy(Collision2D collision)
    {
        collision.gameObject.GetComponent<Collider2D>().enabled = false;
        rb.AddForce(Vector2.up * killPower / 3, ForceMode2D.Impulse);
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * killPower, ForceMode2D.Impulse);
        collision.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
        Destroy(collision.gameObject, 4);
    }

    void Hit()
    {
        wasHit = true;
        StartCoroutine(Blink());
    }

    void Question(Collision2D collision)
    {
        int rand = Random.Range(0, 2);
        Vector3 location = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 1, collision.gameObject.transform.position.z);

        if (rand == 0)
            Instantiate(coin, location, Quaternion.identity);
        else
            if (PlayerPrefs.GetInt("powerUp", 0) == 1)
            Instantiate(flower, location, Quaternion.identity);
        else
            Instantiate(mushroom, location, Quaternion.identity);
        collision.gameObject.tag = "hit";
        collision.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    void Mushroom(Collision2D collision)
    {
        Destroy(collision.gameObject);
        audio.PlayOneShot(powerUpSound);
        PlayerPrefs.SetInt("powerUp", 1);
        anim.SetLayerWeight(1, 1);
        collider.size = new Vector2(collider.size.x, collider.size.y * 2);
        //anim.SetTrigger("Transform");

    }

    void Flower(Collision2D collision)
    {
        audio.PlayOneShot(powerUpSound);
        PlayerPrefs.SetInt("powerUp", 2);
        Destroy(collision.gameObject);
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(4);
        wasHit = false;
        renderer.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsEnemy())
            Enemy(collision);
        else if (collision.gameObject.CompareTag("enemy") && !wasHit)
            Hit();
        else if (IsQuestion())
            Question(collision);
        else if (collision.gameObject.CompareTag("mushroom"))
            Mushroom(collision);
        else if (collision.gameObject.CompareTag("flower"))
            Flower(collision);
    }
}