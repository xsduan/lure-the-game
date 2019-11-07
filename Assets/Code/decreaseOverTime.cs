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
        health = maxHealth;// set health to maximum
        readjustTime = decreaseTime;// set readjust time
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = maxHealth;// sets max value of health bar
        healthBar.value = health;// sets heath bar to your health
    }
    private void Update()
    {
        timeElapsed += Time.deltaTime;// update the time elapsed
        if(Convert.ToInt32(timeElapsed) == decreaseTime)// if elapsed time matches the decrease time period
        {
            decreaseTime += readjustTime;// readjust to prevent repeating
            healthBar.value--;// minus 1% off health bar
        }
    }
}
