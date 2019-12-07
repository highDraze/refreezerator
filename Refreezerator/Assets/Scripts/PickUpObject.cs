﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickUpObject : MonoBehaviour
{
    public Rigidbody rb;
    public float temperature = 0;
    public bool inFridge;
    public float timeToChangeState;
    public float maxFreezeTime;
    public float currentFreezeTime;
    public bool isFrozen;
    public SpriteRenderer renderer;
    public GameObject ice;

    public float distance = 0.1f;

    
    private bool isFlying;

    // Start is called before the first frame update
    public void Start()
    {
        setUpIce();
        setFreeze(false);
        isFrozen = false;
    }

    // Update is called once per frame

    void Update()
    {
        setTemperature();
        
        if (isFlying)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.down);
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if (hit.collider.tag == "floor" && hit.distance < distance)
                {    
                    print("Back on floor");
                    gameObject.layer = 8;
                    isFlying = false;
                } else if (hit.collider.tag == "enemy")
                {
                    Stun stun = hit.collider.GetComponent<Stun>();
                    if (stun)
                    {
                        stun.stun();
                    }
                }
            }
        }
    }

    public void setInvisible(bool invisible)
    {
        renderer.enabled = !invisible;
    }

    public void setRigidbody(bool on)
    {
        GetComponent<Collider>().enabled = on;
        GetComponent<Rigidbody>().useGravity = on;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    protected void setTemperature()
    {
        if (inFridge)
        {
            if (temperature > 0)
            {
                temperature -= Time.deltaTime / timeToChangeState;
            }
            else if (temperature < 0 && !isFrozen)
            {
                temperature = 0;
                currentFreezeTime = maxFreezeTime;
                isFrozen = true;
            }
        }
        else
        {
            if (currentFreezeTime <= 0)
            {
                if (isFrozen)
                {    
                    setFreeze(false);
                    isFrozen = false;
                }
                if (temperature < 1)
                {
                    temperature += Time.deltaTime / timeToChangeState;
                }
            }
            else
            {
                currentFreezeTime -= Time.deltaTime;
            }
        }
    }

    private void setUpIce()
    {
        ice = Instantiate(ice, Vector3.zero, Quaternion.identity);
        ice.transform.SetParent(this.transform);
        ice.transform.localPosition = Vector3.zero;
        ice.transform.localRotation = Quaternion.Euler(45.0f, 0, 0);
    }


    public void setFreeze(bool freezed)
    {
        foreach (var render in ice.GetComponentsInChildren<SpriteRenderer>())
        {
            render.enabled = freezed;
        }
    }
    
    
    public void throwObject()
    {
        gameObject.layer = 11;
        isFlying = true;
    }
}
