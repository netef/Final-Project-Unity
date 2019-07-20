using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayScript : MonoBehaviour
{
    public GameObject instructions;
    bool show;
    void Start()
    {
        show = false;
    }

    void Update()
    {
        instructions.SetActive(show);
    }

    public void Click()
    {
        show = !show;
    }
}