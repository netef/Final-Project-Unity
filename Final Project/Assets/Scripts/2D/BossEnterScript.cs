using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnterScript : MonoBehaviour
{
    public GameObject wall1, wall2, mario, mainCamera, bossCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mario.GetComponent<MarioScriptNew>().enabled = false;
            mario.GetComponent<Animator>().enabled = false;
            mainCamera.SetActive(false);
            bossCamera.SetActive(true);
            wall1.SetActive(true);
            wall2.SetActive(true);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        mario.GetComponent<MarioScriptNew>().enabled = true;
        mario.GetComponent<Animator>().enabled = true;
        Destroy(gameObject);
    }
}