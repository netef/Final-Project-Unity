using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinCountScript : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Coins2D", 0);
    }
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Coins2D", 0) + "";
    }
}