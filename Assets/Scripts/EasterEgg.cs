using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasterEgg : MonoBehaviour
{
    bool turnOn;
    public Image image;
    public void show()
    {
        turnOn = true;
    }

    private void Update()
    {
        if (turnOn)
        {
            Color c = image.color;
            c.a += Time.deltaTime;
            image.color = c;
            if (image.color.a >= 1)
            {
                turnOn = false;
            }
        }
        else
        {
            if (image.color.a > 0) { }
            Color c = image.color;
            c.a -= Time.deltaTime;
            image.color = c;
        }
    }
}
