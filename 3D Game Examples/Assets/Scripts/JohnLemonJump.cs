using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnLemonJump : MonoBehaviour
{

    private Rigidbody playerRb;

    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;

    public float turnSpeed = 20f;

    public float movementSpeed;

    private Rigidbody _rigidbody;

    private Vector3 _movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {

        _rigidbody = GetComponent<Rigidbody>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;



    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isOnGround == true)
        {

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

        }

    }


    void FixedUpdate()
    {


        float horizontal = Input.GetAxis("Horizontal");

        float vertical = Input.GetAxis("Vertical");

        _movement.Set(horizontal, 0f, vertical);
        //_movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        _rigidbody.MovePosition(_rigidbody.position + _movement * movementSpeed * Time.deltaTime);
        _rigidbody.MoveRotation(m_Rotation);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, _movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

}

