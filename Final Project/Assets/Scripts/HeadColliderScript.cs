using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadColliderScript : MonoBehaviour
{
    public GameObject coin;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            int rand = Random.Range(0, 4);
            if (rand == 0)
                Instantiate(coin, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}