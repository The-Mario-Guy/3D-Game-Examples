using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Proto1 : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 8;
    public float turnSpeed = 100;
    private float _horizontalInput;
    private float _verticalInput;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        //Moves da car
        transform.Translate(Vector3.forward * speed * _verticalInput * Time.deltaTime);
        //da turn
        transform.Rotate(Vector3.up, turnSpeed * _horizontalInput * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R))
        {
            //Grabs the current scene and relodes it
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
