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
    public Animator player;
    public bool isDead = false;
    public Slider slider1; //connected the slider

    public float healthDrain;

    void Start()
    {
        health = MaxHealth;
        HealthBar = GetComponent<Slider>();
        player = GetComponent<Animator>();
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = health;
        isDead = false;
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
        if (health <= 0)
        {
            isDead = true;
            Died();
        }

        player.SetBool("isDead", isDead);
    }

    IEnumerator Died()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}