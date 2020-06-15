using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Vector3 movement; // Y-Axis should always be zero
    private Vector3 throwVec;
    public float movementSpeed;
    public Rigidbody rb;
    private PickUpObject objectInRange;
    public Fridge fridge;
    public float throwForce;
    public Vector3 lookingDirection;
    public EasterEgg easteregg;
    public KeyCode lastKey;
    public Image currentItemImage;
    public icicleSlider slider;
    public Animator animator;

    private SpriteRenderer renderer;
    private bool isWalking;
    private float isInteracting;

    private static Vector3 startPos;
    private static PlayerController fakeThis;

    void Start()
    {
        slider = GameObject.Find("icicle").GetComponent<icicleSlider>();
        currentItemImage = GameObject.Find("ItemSlot").GetComponent<Image>();
        animator = GetComponentInChildren<Animator>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        startPos = transform.position;
        fakeThis = this;
    }

    void Update()
    {
        getInput();
        move();

        if (fridge.currentObject != null)
        {
            fridge.currentObject.transform.localPosition = Vector3.up;
        }

        //getDirection();
        checkEasterEgg();
        if (fridge.currentObject != null)
        {
            slider.value = 1 - fridge.currentObject.temperature;
        }
        else
        {
            slider.value = 0;
        }
    }

    /// <summary>
    /// Input is transformed to movement
    /// </summary>
    private void getInput()
    {
        Vector3 movement = Vector3.zero;
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        movement = movement.normalized;
        Vector3 lookingDirection = getDirection(movement);


        if (movement.magnitude > 0.1)
        {
            throwVec = movement.normalized * throwForce;
            
        }

        movement *= movementSpeed;
        
        bool doesInteract = Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Interact");
        if (doesInteract)
        {
            if (fridge.currentObject != null)
            {
                print(movement);

                throwOut();
                print(movement);
            }
            else
            {
                pickUp();
            }

            isInteracting = 0.2f;
        }
        


        bool walkChange = (movement.magnitude > 0) ^ isWalking;
        isWalking = (movement.magnitude > 0);

        bool lookChange = lookingDirection != this.lookingDirection;
        if (isInteracting > 0)
        {
            isInteracting -= Time.deltaTime;
        }

        this.lookingDirection = lookingDirection;
        this.movement = movement;

        animator.SetFloat("interact", isInteracting);
        animator.SetBool("change", walkChange || lookChange || doesInteract);
        setAnimatorValues();
    }

    private void checkEasterEgg()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastKey = KeyCode.S;
        }
        else if (Input.GetKeyDown(KeyCode.G) && lastKey == KeyCode.S)
        {
            lastKey = KeyCode.G;
        }
        else if (Input.GetKeyDown(KeyCode.J) && lastKey == KeyCode.G)
        {
            lastKey = KeyCode.Alpha0;
            easteregg.show();
        }
    }

    private Vector3 getDirection(Vector3 movement)
    {
        if (movement.x == 0f && movement.z == 0f)
        {
            return this.lookingDirection;
        }
        if (Mathf.Abs(movement.z) > Mathf.Abs(movement.x))
        {
            return new Vector3(0, 0, Mathf.Sign(movement.z));
        } else 
        {
            return new Vector3(Mathf.Sign(movement.x), 0, 0);
        }

    }

    /// <summary>
    /// Moves the player in 3d Space
    /// </summary>
    private void move()
    {
        rb.velocity = movement;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PickUpObject>() != null)
        {
            objectInRange = collision.gameObject.GetComponent<PickUpObject>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PickUpObject>() != null)
        {
            objectInRange = null;
        }
    }

    private void pickUp()
    {
        if (objectInRange != null)
        {
            fridge.currentObject = objectInRange;
            objectInRange = null;
            fridge.currentObject.setInvisible(true);
            fridge.currentObject.setFreeze(false);
            fridge.currentObject.setRigidbody(false);
            fridge.currentObject.transform.parent = transform;
            fridge.currentObject.transform.localPosition = Vector3.down * 100;
            fridge.currentObject.inFridge = true;
            Color c = currentItemImage.color;
            c.a = 1;
            currentItemImage.color = c;
            currentItemImage.sprite = fridge.currentObject.renderer.sprite;
            fridge.full = true;
            fridge.veggie = fridge.currentObject.GetComponent<Vegetable>() != null;
        }
    }

    private void throwOut()
    {
        fridge.currentObject.setInvisible(false);
        if (fridge.currentObject.isFrozen)
        {
            fridge.currentObject.setFreeze(true);
        }
        fridge.currentObject.throwObject();
        fridge.currentObject.setRigidbody(true);
        fridge.currentObject.transform.parent = null;
        fridge.currentObject.transform.position = fridge.currentObject.transform.position + this.movement.normalized;
        fridge.currentObject.inFridge = false;
        fridge.currentObject.rb.AddForce(throwVec, ForceMode.Impulse);

        fridge.full = false;
        fridge.veggie = false;
        fridge.currentObject = null;
        Color c = currentItemImage.color;
        c.a = 0;
        currentItemImage.color = c;
    }

    public static void GameOver()
    {
        if (fakeThis.fridge.full)
        {
            fakeThis.throwOut();
        }
        fakeThis.transform.position = startPos;
        //Highscore mechanic; execute on player death or return to safety		
        /*WriteString();
        Home.resetVeggieCount();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        */
    }

    static void WriteString()
    {
        string path = "Assets/highscore.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(Home.getVeggie() + ";");
        writer.Close();
    }

    private void setAnimatorValues()
    {
        animator.SetBool("walking", movement.magnitude > 0.0);
        animator.SetFloat("x", lookingDirection.x);
        animator.SetFloat("z", lookingDirection.z);
        renderer.flipX = lookingDirection.x < 0;
    }

   
}