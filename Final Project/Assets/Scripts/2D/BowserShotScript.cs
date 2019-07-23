using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BowserShotScript : MonoBehaviour
{
    Rigidbody2D rb;
    BossScript bowser;
    float speed;
    int direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bowser = GameObject.Find("Bowser").GetComponent<BossScript>();
        speed = 25f;
        direction = bowser.direction;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }

    void GameOver(Collision2D collision)
    {
        collision.gameObject.GetComponent<Collider2D>().enabled = false;
        collision.gameObject.GetComponent<Animator>().enabled = false;
        collision.gameObject.GetComponent<MarioScriptNew>().enabled = false;
        Debug.Log("1");
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("2");
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GameOver(collision);
        else
            Destroy(gameObject);
    }
}