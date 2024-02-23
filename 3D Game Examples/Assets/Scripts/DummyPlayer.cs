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
    public bool isDead = false;
    public bool levelEnd = false;
    public bool hasGoal = true;
    private Vector3 _movement;
    private Rigidbody _rigidbody;
    private Quaternion _rotation = Quaternion.identity;
    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);
    private Rigidbody playerRb;
    private Vector3 _startingPos;
    private GameObject[] _collectibles;
    public GameObject exitDoor;
    public GameObject exitPlat;
    public GameObject health;

    public float coins;
    public float lives = 4;
    private float coinPlus = 1;
    private float livesLost = -1;

    public Camera mainCamera;
    public ParticleSystem walkDust;
    public Camera objCamerea;

    public TextMeshProUGUI livesCounter;
    public TextMeshProUGUI coinCounter;

    Animator _animator;
    Animator _doorAni;
    Animator _exitAni;
    void Start()
    {
        Physics.gravity = _defaultGravity;
        _animator = GetComponent<Animator>();
        _doorAni = exitDoor.GetComponent<Animator>();
        _exitAni = exitPlat.GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        _startingPos = transform.position;
        //_collectibles = GameObject.FindGameObjectsWithTag("Eggs");
        mainCamera.gameObject.SetActive(true);
        objCamerea.gameObject.SetActive(false);
        hasGoal = true;
        health.SetActive(true);

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
        _animator.SetBool("levelEnd", levelEnd);
        _doorAni.SetBool("hasGoal", hasGoal);
        _exitAni.SetBool("hasGoal", hasGoal);
        

        _rigidbody.MovePosition(_rigidbody.position + _movement * moveSpeed * Time.deltaTime);
        _rigidbody.MoveRotation(_rotation);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, _movement, turnSpeed * Time.deltaTime, 0f);
        _rotation = Quaternion.LookRotation(desiredForward);
    }

    void Update()
    {
        livesCounter.text = lives.ToString();
        coinCounter.text = coins.ToString();
      

        if (Input.GetKeyDown(KeyCode.Z) && IsOnGround == true)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsOnGround = false;
            isJumping = true;
        }

        if (lives == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (coins == 20)
        {
            StartCoroutine(camObj());
            coins = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
            isJumping = false;

        }
        if (collision.gameObject.CompareTag("Death"))
        {
            lives += livesLost;
            transform.position = _startingPos;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {

            coins += coinPlus;

            Object.Destroy(other.gameObject);

        }
        if (other.gameObject.CompareTag("End") && hasGoal == false)
        {
            levelEnd = true;
            health.SetActive(false);
            moveSpeed = 0;
            turnSpeed = 0;
            jumpForce = 0;
        }
    }
        void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(_rotation);
        walkDust.Play();
    }

    IEnumerator camObj()
    {
       
        {
            mainCamera.gameObject.SetActive(false);
            objCamerea.gameObject.SetActive(true);
            hasGoal = false;
            yield return new WaitForSeconds(5.2f);
            mainCamera.gameObject.SetActive(true);
            objCamerea.gameObject.SetActive(false);
            
        }
            yield break;
    }

}