using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLives : MonoBehaviour
{
    public static int livesAmount = 3;
    int internalLives;
    public GameObject lifeTextBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        internalLives = livesAmount;
        lifeTextBox.GetComponent<UnityEngine.UI.Text>().text = "" + internalLives;
    }
}
