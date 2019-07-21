using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 180);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GetComponent<SpriteRenderer>().enabled)
        {
            PlayerPrefs.SetInt("Coins2D", PlayerPrefs.GetInt("Coins2D", 0) + 1);
            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, GetComponent<AudioSource>().clip.length);
        }
    }
}