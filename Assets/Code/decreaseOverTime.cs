using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class decreaseOverTime : MonoBehaviour
{
    public Slider healthBar;
    private float health;
    public float maxHealth;
    public int decreaseTime;
    private int readjustTime;
    private float timeElapsed;

    private void Start()
    {
        health = maxHealth;
        readjustTime = decreaseTime;
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(Convert.ToInt32(timeElapsed) == decreaseTime)
        {
            decreaseTime += readjustTime;
            healthBar.value--;
        }
    }
}
