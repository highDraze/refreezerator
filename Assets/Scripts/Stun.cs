using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using System.Collections;

public class Stun : MonoBehaviour
{
    public float timeToChangeState;
    private bool isStunned;

    private float getSpeed;

    private float stunTime;
    private new SpriteRenderer renderer;


    private void Start()
    {
        stunTime = 0;
        isStunned = false;
        renderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime / timeToChangeState;
        }
        else if (isStunned)
        {
            isStunned = false;
            renderer.color = Color.white;
            if (GetComponent<Lizard>() != null)
            {
                GetComponent<Lizard>().agent.speed = getSpeed;
            }
            else if (GetComponent<Lizard_Big>() != null)
            {
               GetComponent<Lizard_Big>().agent.speed = getSpeed;
            }
        }
    }
    
    public void stun()
    {
        print("STUNNED");
        stunTime = 1;
        isStunned = true;
        renderer.color = Color.white;
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
        foreach (Behaviour childCompnent in this.gameObject.GetComponentsInChildren<Behaviour>())
            childCompnent.enabled = false;
        this.transform.rotation = Quaternion.Euler(0, 0f, 90f);

        StartCoroutine(Death(this));    
        
        

    }
    
    
 
    IEnumerator Death(Stun stun) {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }


    public bool getStunned()
    {
        return isStunned;
    }

}