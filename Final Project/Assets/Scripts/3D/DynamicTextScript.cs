using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTextScript : MonoBehaviour
{
    public GameObject text;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(true);
            Destroy(text, 3);
        }
    }
}