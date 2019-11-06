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
        inventoryBar.fillAmount = fillAmount/20;
        count.text = Convert.ToString(fillAmount);
    }
}
