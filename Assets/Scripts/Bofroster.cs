using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bofroster : MonoBehaviour
{  public GameObject prefab;
    public float spawningRate = 30;
    private float currentSpawnTimer = 1000;

    public GameObject myVeggie = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myVeggie == null){
        currentSpawnTimer = currentSpawnTimer+ Time.deltaTime;
        if (currentSpawnTimer >= spawningRate)
        { 
            //Debug.Log("testydaSasdadsads");
             myVeggie = (GameObject) Instantiate(prefab, transform.position, Quaternion.identity);
             currentSpawnTimer = 0 + Random.Range(0, 5);
        }
        }
    }
}
