using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {
    public Text count;
    public float fillAmount;
    public Image inventoryBar;

    private void Start() {
        inventoryBar.fillAmount = fillAmount / 20; // divides by 20, which is the max capacity of the inventory
        count.text = Convert.ToString(fillAmount); //fill the hexagon by the percentage of the inventory capacity
    }
}