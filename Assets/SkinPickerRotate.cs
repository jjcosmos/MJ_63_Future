using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SkinPickerRotate : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook freeLook;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        freeLook.m_XAxis.Value += Time.deltaTime * 4;
    }
}
