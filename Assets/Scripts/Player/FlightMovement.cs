using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlightMovement : MonoBehaviour
{
    public float flightSpeed;
    public float rollSens = 1;
    public float pitchSens = 1;
    public float yawSens = 1;
    public bool useAutocorrect = false;
    public float autoCorrect = .2f;
    public bool controlEnabled;
    public bool useMouse = false;
    public bool invertY = false;
    public float mouseSens = 2;
    private Rigidbody rb;
    private float hInput, vInput, rInput;
    [SerializeField] Transform myCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controlEnabled = true;
        if(useMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        int invert = invertY ? -1 : 1;
        if(!useMouse){
            hInput = mouseSens * Input.GetAxis("Horizontal"); //yaw
            vInput = mouseSens * invert * Input.GetAxis("Vertical"); //pitch
        }
        else{
            hInput = mouseSens * Input.GetAxis("Mouse X"); //yaw
            vInput = invert * mouseSens * Input.GetAxis("Mouse Y"); //pitch
        }
        rInput = Input.GetAxis("Roll");
        //transform.Rotate(new Vector3(0,0, rInput * rollSens * Time.deltaTime), Space.Self);

        if(!useAutocorrect || !enabled) return;
        
        //roll
        if(rInput < .01f && rInput > -.01f && rb.angularVelocity.magnitude < 0.001f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), autoCorrect * Time.deltaTime);
        }
        //pitch
        return;
        if (vInput < .01f && vInput > -.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z), autoCorrect * Time.deltaTime);
        }
    }

    private void FixedUpdate() 
    {
        if(!controlEnabled) return;

        rb.AddForce(transform.forward * flightSpeed * Mathf.Clamp((1 + (GlobalVars.difficultyManager.currentDifficultyMod/20f)), 0, 2f)  );
        rb.AddTorque(transform.forward * -rInput * rollSens);
        rb.AddTorque(transform.right * vInput * pitchSens);
        //transform.Rotate(new Vector3(0, hInput * yawSens, 0 ), Space.Self);
        //rb.AddTorque(myCamera.up * hInput * yawSens);
        rb.AddTorque(transform.up * hInput * yawSens);
    }
}
