using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] GradientCreator gradient;
    // Start is called before the first frame update
    float gradientsSampled = 0;
    float addedGradients = 0;
    float currentMax =0;
    float currentMin = 1;
    int index = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float val = gradient.GetHorizontalValueAtPixel(index);
        addedGradients += val;
        index ++;

        if(val > currentMax)
        {
            currentMax = val;
        }
        if(val < currentMin)
        {
            currentMin = val;
        }
        //Debug.Log($"Average value is {addedGradients/index}");
        Debug.Log($"{val}, current min at {currentMin}, current max at {currentMax}");
    }
}
