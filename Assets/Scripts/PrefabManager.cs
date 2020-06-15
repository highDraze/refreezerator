using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static PrefabManager instance;

    public GameObject bigLizardPrefab;
    public GameObject babyLizardPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public GameObject getBigLiz()
    {
        return bigLizardPrefab;
    }

    public GameObject getBabyLiz()
    {
        return babyLizardPrefab;
    }
}
