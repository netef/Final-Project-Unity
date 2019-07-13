using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public GameObject coinDisplay;
    public static int coinCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinDisplay.GetComponent<UnityEngine.UI.Text>().text = "Coins : " + coinCount;
    }
}
