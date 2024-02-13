using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototype3 : MonoBehaviour
{
    private Rigidbody playerRb;

    public float jumpForce;
    public float Gravity;
    public bool isOnGround = true;
    float horizontalInput;
    float verticalInput;
    
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= Gravity;
    }



    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Z))
        { 
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
        Vector3 movement = new Vector3(horizontalInput, Gravity, verticalInput);
        playerRb.AddForce(movement * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
          
        }
       
    }
}
