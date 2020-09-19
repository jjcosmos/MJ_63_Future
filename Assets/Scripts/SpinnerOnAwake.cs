using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerOnAwake : MonoBehaviour
{
    [SerializeField] Rigidbody myRB;
    [SerializeField] bool localZ;
    [SerializeField] bool justTurn = true;
    private int maxVel = 10;
    private int maxVel2 = 1;
    private Vector3 rotationPerFixed;
    
    private void Awake() 
    {
        System.Random r = new System.Random((int)transform.position.z);
        rotationPerFixed = new Vector3(0,0, r.Next(-maxVel, maxVel)/10f  );
    }

    private void FixedUpdate() {
        if(justTurn)
        {
            transform.Rotate(rotationPerFixed);
        }
    }
}
