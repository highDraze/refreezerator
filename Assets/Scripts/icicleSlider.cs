using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icicleSlider : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;
    RectTransform rectTransform;
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, value);
    }
}
