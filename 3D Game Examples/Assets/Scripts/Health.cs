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
    public Slider slider1; //connected the slider

    public float healthDrain;

    void Start()
    {
        health = MaxHealth;
        HealthBar = GetComponent<Slider>();

        HealthBar.maxValue = MaxHealth;
        HealthBar.value = health;
        //lowBattery.SetActive(false);

    }

    void Update()
    {
        health -= healthDrain;
        HealthBar.value = health;
        if (health < 30)
        {
            slider1Fill.color = Color.Lerp(Color.red, Color.green, slider1.value / 100);
            lowBattery.SetActive(true);
        }
        else if (health > 30)
        {
            lowBattery.SetActive(false);
        }
        if (health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}