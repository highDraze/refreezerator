using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BofrostMachine : MonoBehaviour
{
    public static BofrostMachine instance;
    public GameObject prefab;
	public List<GameObject> allTheVeggies = new List<GameObject>();
    public float spawningRate = 10;
    private float currentSpawnTimer = 1000;
    // Start is called before the first frame update
    private void Awake()
    {
     if(instance == null)
        {
            instance = this;
        }   
    }

    // Update is called once per frame
    void Update()
    {
	currentSpawnTimer = currentSpawnTimer+ Time.deltaTime;
    if(currentSpawnTimer >= spawningRate){
        GameObject go = (GameObject) Instantiate(prefab, new Vector3(0 , 5, 0), Quaternion.identity);
            allTheVeggies.Add(go);
		currentSpawnTimer = 0 + Random.Range(0,5);
}
        
    }
}
