using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto4 : MonoBehaviour
{
    private float speed = 10.0f;
    private Rigidbody playerRb;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        //powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(transform.forward * speed * verticalInput * horizontalInput* Time.deltaTime);
    }
}
