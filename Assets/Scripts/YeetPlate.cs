using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeetPlate : MonoBehaviour
{
    public GameObject yeetable;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Destroy(yeetable);
        }
    }
}
