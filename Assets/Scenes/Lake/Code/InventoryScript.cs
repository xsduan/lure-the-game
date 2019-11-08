using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public Image inventoryBar;
    public float fillAmount;
    public Text count;

    private void Start()
    {
        inventoryBar.fillAmount = fillAmount/20;// divides by 20, which is the max capacity of the inventory
        count.text = Convert.ToString(fillAmount);//fill the hexagon by the percentage of the inventory capacity
    }
}
