using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneKeyListener : MonoBehaviour
{
   [SerializeField] SceneTransitionController controller;
   [SerializeField] KeyCode code;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(code))
        {
            controller.SwapScene();
        }   
    }
}
