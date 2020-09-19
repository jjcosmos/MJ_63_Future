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
    public bool useAutocorrect = true;
    public float autoCorrect = .2f;
    public bool controlEnabled;

    private Rigidbody rb;
    private float hInput, vInput, rInput;

    [SerializeField] Transform myCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controlEnabled = true;
    }

    void Update()
    {
        hInput = Input.GetAxis("Horizontal"); //yaw
        vInput = Input.GetAxis("Vertical"); //pitch
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

        rb.AddForce(transform.forward * flightSpeed * (1 + (GlobalVars.difficultyManager.currentDifficultyMod/10f)));
        rb.AddTorque(transform.forward * -rInput * rollSens);
        rb.AddTorque(transform.right * vInput * pitchSens);
        //transform.Rotate(new Vector3(0, hInput * yawSens, 0 ), Space.Self);
        //rb.AddTorque(myCamera.up * hInput * yawSens);
        rb.AddTorque(transform.up * hInput * yawSens);
    }
}
