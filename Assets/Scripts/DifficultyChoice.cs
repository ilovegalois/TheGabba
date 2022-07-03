using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyChoice : MonoBehaviour
{
    TMP_Dropdown dd;

    private void Start()
    {
        dd = GetComponent<TMP_Dropdown>();
    }
    public void DiffChoice(int data)
    {
        switch (dd.value)
        {
            case 0:
                {
                    DataController.numberOfZombies = 5;
                    break;
                }
            case 1:
                {
                    DataController.numberOfZombies = 10;
                    break;
                }
            case 2:
                {
                    DataController.numberOfZombies = 25;
                    break;
                }
            case 3:
                {
                    DataController.numberOfZombies = 50;
                    break;
                }
            case 4:
                {
                    DataController.numberOfZombies = 100;
                    break;
                }
        }
    }
}
