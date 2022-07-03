using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeChoice : MonoBehaviour
{
    TMP_Dropdown dd;

    private void Start()
    {
        dd = GetComponent<TMP_Dropdown>();
    }
    public void TmChoice(int data)
    {
        switch (dd.value)
        {
            case 0:
                {
                    DataController.roundTime = 1200;
                    break;
                }
            case 1:
                {
                    DataController.roundTime = 900;
                    break;
                }
            case 2:
                {
                    DataController.roundTime = 720;
                    break;
                }
            case 3:
                {
                    DataController.roundTime = 480;
                    break;
                }
            case 4:
                {
                    DataController.roundTime = 300;
                    break;
                }
        }
    }
}
