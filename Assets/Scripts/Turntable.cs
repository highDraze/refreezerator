using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turntable : MonoBehaviour
{
    public Vector3 speed = new Vector3(0f,1f,0f);
    public bool bounce = false;

    void Update()
    {
        if (bounce)
            this.transform.Rotate(Mathf.Sin(Time.time) * speed);
        else
            this.transform.Rotate(Time.deltaTime*speed);
    }
}
