using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitListener : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
