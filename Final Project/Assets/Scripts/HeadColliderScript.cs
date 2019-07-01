using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadColliderScript : MonoBehaviour
{
    public GameObject coin;
    public GameObject mushroom;
    public GameObject flower;
    public Sprite sprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ground":
                {
                    if (PlayerPrefs.GetInt("powerUp", 0) != 0)
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
                    Vector3 location = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 1, collision.gameObject.transform.position.z);
                    if (PlayerPrefs.GetInt("powerUp", 0) == 1)
                        Instantiate(flower, location, Quaternion.identity);
                    else
                        Instantiate(mushroom, location, Quaternion.identity);

                    collision.gameObject.tag = "hit";
                    collision.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                    break;
                }
        }
    }
}