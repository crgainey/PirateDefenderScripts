using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] GameManager gameManger;
    
    int currentBalance;
    public int CurrentBalance { get { return currentBalance; } } // aceesss but cant set

    [SerializeField] TextMeshProUGUI displayBalance;

    void Awake()
    {

        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        // this is Absolute makes sure we dont pass any negative values when depositing will turn negative to positive
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();

        if (currentBalance < 0)
        {
            gameManger.LoseMenu();
        }
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }

    

}
