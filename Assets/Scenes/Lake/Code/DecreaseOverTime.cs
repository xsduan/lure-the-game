using System;
using UnityEngine;
using UnityEngine.UI;

public class DecreaseOverTime : MonoBehaviour {
    public int decreaseTime;
    private float health;
    public Image healthBar;
    public float maxHealth;
    private int resetAmount;
    public float subtractAmount;
    private float timeElapsed;

    private void Start() {
        health = maxHealth; // set health to maximum
        healthBar.fillAmount = maxHealth / 100;
        resetAmount = decreaseTime;
    }

    private void Update() {
        timeElapsed += Time.deltaTime; // update the time elapsed
        if (Convert.ToInt32(timeElapsed) == decreaseTime) // if elapsed time matches the decrease time period
        {
            decreaseTime += resetAmount;
            healthBar.fillAmount =
                (health - subtractAmount) /
                100; // allows you to choose what to subtract health by (difficulty settings?)
            health -= subtractAmount;
        }
    }
}