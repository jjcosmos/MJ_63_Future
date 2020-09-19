using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BonkManager : MonoBehaviour
{
    [SerializeField] FlightMovement playerControls;
    [SerializeField] SceneTransitionController controller;
    [SerializeField] CinemachineFreeLook vcam2;
    [SerializeField] GameObject restartPrompt;
    [SerializeField] GameObject toDisable;
    [SerializeField] GameObject toEnable;
 
    private bool lost = false;
    

    private void Start() {
        //freelook = vcam2.GetCinemachineComponent<CinemachineFreeLook>();
    }

    private void OnCollisionEnter(Collision other) {

        if(other.collider.CompareTag("Obstacle") && !lost)
        {
            Debug.Log("Bonk");
            lost = true;
            playerControls.controlEnabled = false;
            Rigidbody playerRB = playerControls.GetComponent<Rigidbody>();
            playerRB.useGravity = true;
            playerRB.drag = 0;
            playerRB.angularDrag = 0;
            playerRB.isKinematic = true;
            vcam2.Priority = 30;
            restartPrompt.SetActive(true);
            GlobalVars.SetHighScore(GlobalVars.scoreTracker.score);
            toDisable.SetActive(false);
            toEnable.SetActive(true);
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
            vcam2.m_XAxis.Value = Time.deltaTime*2 + vcam2.m_XAxis.Value;
        }
    }
    private void Lose()//reset
    {
        restartPrompt.SetActive(false);
        controller.SwapScene();
    }
}
