using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSetup : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() 
    {
        GlobalVars.mainCineCam = GetComponent<CinemachineFreeLook>();
    }
}
