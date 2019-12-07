﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private int veggieCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Vegetable veggie = other.gameObject.GetComponent<Vegetable>();
        if (veggie)
        {
            veggieCount++;
            Destroy(other.gameObject);
            print(veggieCount);
        }
    }
}