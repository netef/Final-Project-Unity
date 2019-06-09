using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadColliderScript : MonoBehaviour
{
    public GameObject coin;
    public GameObject mushroom;
    public GameObject flower;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ground":
                {
                    if(PlayerPrefs.GetInt("isBig", 0) == 1)
                    {
                        int rand = Random.Range(0, 4);
                        if (rand == 0)
                            Instantiate(coin, collision.gameObject.transform.position, Quaternion.identity);
                        Destroy(collision.gameObject);
                    }                  
                    break;
                }

            case "question":
                {
                    if (PlayerPrefs.GetInt("isBig", 0) == 1)
                    {
                        Instantiate(flower, collision.gameObject.transform.position, Quaternion.identity);
                    }
                    Instantiate(mushroom, collision.gameObject.transform.position, Quaternion.identity);
                    break;
                }
        }
    }
}