using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreCountScript : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Score2D", 0);
    }

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score2D", 0) + "";
    }
}