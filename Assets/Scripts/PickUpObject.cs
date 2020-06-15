using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PickUpObject : MonoBehaviour
{
    public Rigidbody rb;
    private Collider collider;
    public float temperature { get; private set; } = 1;
    public bool inFridge;
    public float timeToChangeState;
    public float maxFreezeTime;
    public float currentFreezeTime;
    public bool isFrozen;
    public SpriteRenderer renderer;
    public GameObject ice;

    private float distance = 0.6f;
    private int ogLayer;
    private Lizard lizard;
    
    public bool isFlying { get; private set; } = false;

    // Start is called before the first frame update
    public void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        setUpIce();
        setFreeze(false);
        isFrozen = false;
        ogLayer = gameObject.layer;
        lizard = GetComponent<Lizard>();
        this.collider = GetComponent<Collider>();
}

    // Update is called once per frame

    public void Update()
    {
        setTemperature();
        setFlying();
        if (temperature > 0)
        {
            var col = renderer.color;
            col.a = temperature;
            renderer.color = 
                new Color(
                    temperature / 2 + 0.5f,
                    renderer.color.g,
                    renderer.color.b,
                    renderer.color.a);

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
				
                if(lizard) lizard.agent.enabled = false;
                
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
                    if(lizard) lizard.agent.enabled = true;
                }
                if (temperature < 1)
                {
                    temperature += Time.deltaTime / timeToChangeState;
                    if(lizard) lizard.agent.speed = temperature * lizard.speed;

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

    private void OnTriggerEnter(Collider other)
    {
        if (isFrozen && other.gameObject.tag == "enemy")
        {
            Stun stun = other.GetComponent<Stun>();
            print(stun);
            if (stun)
            {
                stun.stun();
            }
        }
    }


    public void setFlying()
    {
        if (isFlying)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.down, Color.blue, 0.2f);
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if (hit.collider.CompareTag("floor") && hit.distance < distance)
                {
                    gameObject.layer = ogLayer;
                    isFlying = false;
                    this.collider.enabled = true;
                }
            }
        }
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
        this.collider.enabled = false;
    }
}