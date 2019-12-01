using System;
using UnityEngine;
using UnityEngine.UI;

public class SetMoneyCount : MonoBehaviour {
    public int amountOfMoney;
    public Text money;

    private void Start() => money.text = Convert.ToString(amountOfMoney); // set the money value to the value entered
}