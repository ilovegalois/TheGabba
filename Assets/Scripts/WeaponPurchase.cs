using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPurchase : MonoBehaviour
{
    public GameObject nextButton;

    GameObject thisButton;

    void Start()
    {
        thisButton = this.gameObject;
    }

    public void makePurchase()
    {
        int cash = DataController.cashAmount;

        switch (DataController.AllowedWeapons)
        {
            case 0:
                {
                    if (cash >= 2)
                    {
                        DataController.cashAmount -= 2;
                        DataController.AllowedWeapons++;
                    }
                    else
                    {
                        thisButton.SetActive(true);
                        nextButton.SetActive(false);
                    }
                    break;
                }
            case 1:
                {
                    if (cash >= 5)
                    {
                        DataController.cashAmount -= 5;
                        DataController.AllowedWeapons++;
                    }
                    else
                    {
                        thisButton.SetActive(true);
                        nextButton.SetActive(false);
                    }
                    break;
                }
            case 2:
                {
                    if (cash >= 10)
                    {
                        DataController.cashAmount -= 10;
                        DataController.AllowedWeapons++;
                    }
                    else
                    {
                        thisButton.SetActive(true);
                        nextButton.SetActive(false);
                    }
                    break;
                }
        }
    }
}
