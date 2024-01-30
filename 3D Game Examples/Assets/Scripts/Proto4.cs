using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Proto4 : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    public float Gravity;
    private Rigidbody _playerRigidBody;
    public float forceMultiplier;
    // public GameObject winTextObjectShadow;

    private int count;


    void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody>();
     

        // SetCountText ();
    }

    void Update()
    {


        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, Gravity, verticalInput);

        _playerRigidBody.AddForce(movement * forceMultiplier);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }

    
    
      

    }
}
