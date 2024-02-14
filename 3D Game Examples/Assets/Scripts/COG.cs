using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class COG : MonoBehaviour
{
    private Rigidbody rb;
    public Transform centerOfGravity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (centerOfGravity)
        {
            rb.centerOfMass = centerOfGravity.localPosition;
        }
    }
}