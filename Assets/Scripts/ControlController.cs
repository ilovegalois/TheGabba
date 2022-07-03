using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlController : MonoBehaviour
{
    public Transform moveStick;
    public Transform fireStick;

    private void Start()
    {
        if(DataController.moveStick != null && DataController.fireStick != null) 
        {
            moveStick = DataController.moveStick;
            fireStick = DataController.fireStick;
        }
    }
}
