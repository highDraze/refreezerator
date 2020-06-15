using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyLizardCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PickUpObject>() != null)
        {
            if (collision.gameObject.GetComponent<PickUpObject>().isFrozen)
            {
                // Stun
            }
            else
            {
                Instantiate(PrefabManager.instance.bigLizardPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }else if(collision.gameObject.tag == "Player")
        {
            PlayerController.GameOver();
        } 
    }
}
