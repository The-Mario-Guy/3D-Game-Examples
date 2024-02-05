using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        Physics.gravity = _defaultGravity;

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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

