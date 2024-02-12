using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class JohnLemonJump : MonoBehaviour
{

    private Rigidbody playerRb;

    public float jumpForce;
    public float gravityModifier;
    private float OoB = -1.1f;
    public bool isOnGround = true;
    public bool isAtCheckPoint = false;
    public float lives = 4;
    public float coins;
    public float eggsCounter;
    private float coinPlus = 1;
    private float livesLost = -1;
    private GameObject[] _collectibles;

    public TextMeshProUGUI livesCounter;
    public TextMeshProUGUI coinCounter;
    public TextMeshProUGUI eggCounter;
    public GameObject checkPointAreaObject;
    public GameObject FinishedAreaObject;


    public float turnSpeed = 20f;

    public float movementSpeed;

    private Rigidbody _rigidbody;

    private Vector3 _movement;

    private Vector3 _defaultGravity = new Vector3(0f, -9.81f, 0f);

    private Vector3 _startingPos;
    Quaternion m_Rotation = Quaternion.identity;

    void Start()
    {
        Physics.gravity = _defaultGravity;

        _rigidbody = GetComponent<Rigidbody>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        _startingPos = transform.position;
        _collectibles = GameObject.FindGameObjectsWithTag("Collectible-Return");




    }
    void Update()
    {
        livesCounter.text = lives.ToString();
        coinCounter.text = coins.ToString();
        eggCounter.text = eggsCounter.ToString();
        if (Input.GetKeyDown(KeyCode.Z) && isOnGround == true)
        {

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

        }
        //Death
        if (transform.position.y < OoB)
        {
            lives += livesLost;
            ReturningCollectibles();
            transform.position = _startingPos;

        }

        if (lives == 0)
        {
            ReturningCollectibles();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

        }
        if (collision.gameObject.CompareTag("Death"))
        {
            lives += livesLost;
            ReturningCollectibles();
            transform.position = _startingPos;

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer m = checkPointAreaObject.GetComponent<MeshRenderer>();
        m.enabled = true;
        if (other.gameObject == checkPointAreaObject)
        {
            isAtCheckPoint = true;
            m.enabled = false;
            _startingPos = checkPointAreaObject.transform.position;
        }
        if (other.gameObject == FinishedAreaObject)
        {
            isAtCheckPoint = false;
            ReturningCollectibles();
            //transform.position = _startingPos;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.gameObject.CompareTag("Coin"))
        {

            coins += coinPlus;

            Object.Destroy(other.gameObject);

        }
        if (other.gameObject.CompareTag("Collectible-Return"))
        {
            eggsCounter++;
            other.gameObject.GetComponent<Collectibles>().HideCollectibles();
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

    void ReturningCollectibles()
    {
        for (int i = 0; i < _collectibles.Length; i++)
        {
            _collectibles[i].SetActive(true);
            _collectibles[i].GetComponent<Collectibles>().ReturnCollectibles();
        }
    }

}
