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
    public GameObject fireBall;
    public Sprite sprite;
    public AudioClip powerUpSound;
    public AudioClip JumpSound;
    public AudioClip pipeSound;
    public float jumpPower;
    public float velocity;
    public float killPower;
    public bool facingRight, wasHit, space;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer renderer;
    AudioSource audio;
    CapsuleCollider2D capsuleCollider;
    float direction;
    Vector2 small = new Vector2(0.12f, 0.16f);
    Vector2 big = new Vector2(0.12f, 0.32f);

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();

        capsuleCollider = gameObject.AddComponent<CapsuleCollider2D>();
        capsuleCollider.size = small;

        direction = 0;
        facingRight = true;
        wasHit = false;
        space = false;
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

        if (PlayerPrefs.GetInt("powerUp", 0) == 2 && Input.GetKeyDown(KeyCode.Z))
            FireBall();
    }

    private void FixedUpdate()
    {
        //moves mario
        rb.velocity = new Vector2(velocity * direction, rb.velocity.y);
    }

    void FireBall()
    {
        //creates a fireball infront of mario
        Vector3 location = new Vector3(transform.position.x, transform.position.y, 0);
        var ignore = Instantiate(fireBall, transform.position, Quaternion.identity);
        Physics2D.IgnoreCollision(ignore.GetComponent<CircleCollider2D>(), capsuleCollider);
    }
    //sends a raycast to check if mario is on the ground
    bool IsGrounded()
    {
        Vector2 position = transform.position - new Vector3(0, renderer.bounds.size.y / 2, 0);
        Vector2 direction = Vector2.down;
        float radius = 0.5f;
        float distance = 0.1f;

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
        else if (IsGrounded())
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
        Vector2 position = transform.position - new Vector3(0, renderer.bounds.size.y / 2, 0);
        Vector2 direction = Vector2.down;
        float radius = 0.2f;
        float distance = 0.1f;

        RaycastHit2D hit = Physics2D.CircleCast(position, radius, direction, distance, enemyLayer);
        if (hit.collider != null)
            return true;
        return false;
    }

    bool IsQuestion()
    {
        Vector2 position = transform.position + new Vector3(0, renderer.bounds.size.y / 2, 0);
        Vector2 direction = Vector2.up;
        float radius = 0.01f;
        float distance = 0.1f;

        RaycastHit2D hit = Physics2D.CircleCast(position, radius, direction, distance, groundLayer);
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

    void Hit(Collision2D collision)
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        wasHit = true;
        Shrink();
        StartCoroutine(Blink());
    }

    void Question(Collision2D question)
    {
        int rand = Random.Range(0, 2);
        Vector3 location = new Vector3(question.gameObject.transform.position.x, question.gameObject.transform.position.y + 1, question.gameObject.transform.position.z);

        if (rand == 0)
            Instantiate(coin, location, Quaternion.identity);
        else
            if (PlayerPrefs.GetInt("powerUp", 0) == 1)
            Instantiate(flower, location, Quaternion.identity);
        else
            Instantiate(mushroom, location, Quaternion.identity);
        question.gameObject.tag = "hit";
        question.gameObject.GetComponent<Animator>().SetLayerWeight(1, 1);
    }
    void Mushroom(Collision2D collision)
    {
        Destroy(collision.gameObject);
        audio.PlayOneShot(powerUpSound);
        if (PlayerPrefs.GetInt("powerUp", 0) == 0)
        {
            PlayerPrefs.SetInt("powerUp", 1);
            anim.SetLayerWeight(1, 1);
            capsuleCollider.size = big;
        }
    }

    void Flower(Collision2D collision)
    {
        audio.PlayOneShot(powerUpSound);
        //if mario takes the flower when he is small he can revert to medium size when getting hit
        if (PlayerPrefs.GetInt("PowerUp", 0) == 0)
        {
            capsuleCollider.size = big;
            anim.SetLayerWeight(1, 1);
        }
        anim.SetLayerWeight(2, 1);
        PlayerPrefs.SetInt("powerUp", 2);
        Destroy(collision.gameObject);
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(4);
        wasHit = false;
        renderer.enabled = true;
    }

    void Shrink()
    {
        switch (PlayerPrefs.GetInt("powerUp", 0))
        {
            case 0:
                return;
            case 1:
                PlayerPrefs.SetInt("powerUp", 0);
                capsuleCollider.size = small;
                anim.SetLayerWeight(1, 0);
                return;
            case 2:
                PlayerPrefs.SetInt("powerUp", 1);
                anim.SetLayerWeight(2, 0);
                return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsEnemy() && !space)
            Enemy(collision);
        else if (collision.gameObject.CompareTag("enemy") ||
             collision.gameObject.CompareTag("kill") ||
             collision.gameObject.CompareTag("flowerEnemy") && !wasHit)
            Hit(collision);
        else if (IsQuestion())
            Question(collision);
        else if (collision.gameObject.CompareTag("mushroom"))
            Mushroom(collision);
        else if (collision.gameObject.CompareTag("flower"))
            Flower(collision);
    }
}