using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quicksand : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            Destroy(other.gameObject);
        }
        else
        {
            PlayerController.GameOver();
        }
    }
}
