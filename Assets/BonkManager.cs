using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BonkManager : MonoBehaviour
{
    [SerializeField] FlightMovement playerControls;
    [SerializeField] SceneTransitionController controller;
    [SerializeField] CinemachineVirtualCamera vcam2;

    private bool lost = false;
    private CinemachineTrackedDolly dolly;

    private void Start() {
        dolly = vcam2.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    private void OnCollisionEnter(Collision other) {

        if(other.collider.CompareTag("Obstacle") && !lost)
        {
            Debug.Log("Bonk");
            lost = true;
            playerControls.controlEnabled = false;
            vcam2.Priority = 20;
        }
        else{
            Debug.Log($"Hit mystery {other.collider.name}");
        }
    }

    private void Update() {
        if(lost && Input.GetKeyDown(KeyCode.Space))
        {
            Lose();
        }
        if(lost)
        {
            dolly.m_PathPosition += Time.deltaTime;
        }
    }
    private void Lose()//reset
    {
        controller.SwapScene();
    }
}
