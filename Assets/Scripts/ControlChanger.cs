using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChanger : MonoBehaviour
{
    public Transform fireStick;
    public Transform moveStick;

    public void ChangeStick()
    {
        DataController.fireStick = fireStick;
        DataController.moveStick = moveStick;
    }
}
