using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private static int veggieCount = 0;
    public List<GameObject> levels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int getVeggie()
    {
        return veggieCount;
        
    }

    public static void resetVeggieCount()
    {
        veggieCount = 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        Vegetable veggie = other.gameObject.GetComponent<Vegetable>();
        if (veggieCount < levels.Count)
        {
            Destroy(levels[veggieCount]);
        }
        if (veggie)
        {
            veggieCount++;
            Destroy(other.gameObject);
            
        }
    }
}
