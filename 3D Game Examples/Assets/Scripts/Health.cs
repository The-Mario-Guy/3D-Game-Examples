using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public Slider HealthBar;
    public float health;
    public float MaxHealth;// 1000
    public Image slider1Fill;
    public GameObject lowBattery;
    public GameObject smoke;
    public GameObject music;
    public GameObject lowBat;
    public GameObject lights;
    public GameObject lowLights;
    public GameObject emergancyLights;
    public GameObject playerLights;
    public Slider slider1; //connected the slider

    public float healthDrain;

    void Start()
    {
        health = MaxHealth;
        HealthBar = GetComponent<Slider>();
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = health;
        smoke.SetActive(false);
        lowBat.SetActive(false);
    }

    void Update()
    {
        health -= healthDrain;
        HealthBar.value = health;
        if (health < 30)
        {
            //Has low battery
            smoke.SetActive(true);
            slider1Fill.color = Color.Lerp(Color.red, Color.green, slider1.value / 100);
            lowBattery.SetActive(true);
            music.SetActive(false);
            lowBat.SetActive(true);
            lights.SetActive(false);
            lowLights.SetActive(true);
            playerLights.SetActive(false);
            emergancyLights.SetActive(true);
        }
        else if (health > 30)
        {
            //Has over 30 health ss
            smoke.SetActive(false);
            lowBattery.SetActive(false);
            lowBat.SetActive(false);
            lights.SetActive(true);
            lowLights.SetActive(false);
            playerLights.SetActive(true);
            emergancyLights.SetActive(false);
        }
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }

    }
}