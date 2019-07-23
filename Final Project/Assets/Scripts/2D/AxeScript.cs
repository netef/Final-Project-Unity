using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AxeScript : MonoBehaviour
{
    public Transform leftPos, rightPos;
    public GameObject mario, boss, bridge;

    int counter;

    void Start()
    {
        counter = 3;
    }
    void Update()
    {
        if (counter == 0)
            Win();
    }

    void Win()
    {
        mario.GetComponent<Animator>().enabled = false;
        mario.GetComponent<MarioScriptNew>().enabled = false;
        boss.GetComponent<BossScript>().enabled = false;
        boss.GetComponent<Collider2D>().enabled = false;
        Destroy(bridge);
        Destroy(boss, 3);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(5, LoadSceneMode.Single);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && transform.position == rightPos.position)
        {
            transform.position = leftPos.position;
            counter--;
        }

        else if (collision.gameObject.CompareTag("Player") && transform.position == leftPos.position)
        {
            transform.position = rightPos.position;
            counter--;
        }
    }
}