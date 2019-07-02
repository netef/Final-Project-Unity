using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    public GameObject body;
    public GameObject mario;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("hit"))
            body.GetComponent<MarioScript>().isGrounded = true;
        else if (collision.gameObject.CompareTag("enemy") &&
            body.GetComponent<MarioScript>().isGrounded == false)
        {
            mario.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 30, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20000, ForceMode2D.Impulse);
            collision.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
            Destroy(collision.gameObject, 4);
        }
        else if (collision.gameObject.CompareTag("kill"))
        {
            if (PlayerPrefs.GetInt("powerUp", 0) == 0)
            {
                Debug.Log("GameOver");
            }
            else if (PlayerPrefs.GetInt("powerUp", 0) == 1)
            {
                body.GetComponent<MarioScript>().shrink = true;
                PlayerPrefs.SetInt("powerUp", 0);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                PlayerPrefs.SetInt("powerUp", 1);
            }
            body.GetComponent<MarioScript>().canHit = false;
            StartCoroutine(body.GetComponent<MarioScript>().Wait());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("kill") && body.GetComponent<MarioScript>().canHit)
        {
            if (PlayerPrefs.GetInt("powerUp", 0) == 0)
            {
                Debug.Log("GameOver");
            }
            else if (PlayerPrefs.GetInt("powerUp", 0) == 1)
            {
                body.GetComponent<MarioScript>().shrink = true;
                PlayerPrefs.SetInt("powerUp", 0);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                PlayerPrefs.SetInt("powerUp", 1);
            }
            body.GetComponent<MarioScript>().canHit = false;
            StartCoroutine(body.GetComponent<MarioScript>().Wait());
        }
    }
}