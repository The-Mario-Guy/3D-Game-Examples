using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int sceneNumber;
    public float transitionTime = 1f;
    public Animator transition;
    void Start()
    {
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
        
        
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneNumber);
    }

}
