using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class setMoneyCount : MonoBehaviour
{
    public Text money;
    public int amountOfMoney;
    private void Start()
    {
        money.text = Convert.ToString(amountOfMoney);// set the money value to the value entered
    }
}
