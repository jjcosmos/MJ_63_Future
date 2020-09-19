using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private int maxVel = 20;
    private int maxVel2 = 1;
    void Start()
    {
        System.Random r = new System.Random((int)transform.position.z);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(r.Next(-maxVel, maxVel), r.Next(-maxVel, maxVel),r.Next(-maxVel, maxVel));
        rb.angularVelocity = new Vector3(r.Next(-maxVel2, maxVel2), r.Next(-maxVel2, maxVel2), r.Next(-maxVel2, maxVel2));
    }

 
}
