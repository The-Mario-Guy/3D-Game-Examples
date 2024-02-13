using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DummyPlayer : MonoBehaviour
{
    public float turnSpeed = 20;
    public float moveSpeed = 1f;
    public float jumpForce;
    public float gravityModifier = 1f;
    public bool IsOnGround = true;
    public bool isJumping = false;
    private Vector3 _movement;
    private Rigidbody _rigidbody;
    private Quaternion _rotation = Quaternion.identity;
    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);
    private Rigidbody playerRb;
    private Vector3 _startingPos;
    private GameObject[] _collectibles;

    Animator _animator;
    void Start()
    {
        Physics.gravity = _defaultGravity;
        _animator = GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        _startingPos = transform.position;
        //_collectibles = GameObject.FindGameObjectsWithTag("Eggs");




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
        

        _animator.SetBool("isWalking", isWalking);
        _animator.SetBool("isJumping", isJumping);

        _rigidbody.MovePosition(_rigidbody.position + _movement * moveSpeed * Time.deltaTime);
        _rigidbody.MoveRotation(_rotation);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, _movement, turnSpeed * Time.deltaTime, 0f);
        _rotation = Quaternion.LookRotation(desiredForward);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && IsOnGround == true)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsOnGround = false;
            isJumping = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
            isJumping = false;

        }
    }
    void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(_rotation);
    }
}