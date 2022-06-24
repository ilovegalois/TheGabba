using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponManager : MonoBehaviour
{

    public Joystick fireStick;
    public int ActiveWeaponIndex { get; private set; }
    public int ActiveWeaponCount { get; private set; }

    public WeaponManager[] m_WeaponSlots = new WeaponManager[4]; // 4 weapon slots

    public UnityAction<WeaponManager> OnSwitchedToWeapon;
    public UnityAction<WeaponManager, int> OnAddedWeapon;
    public UnityAction<WeaponManager, int> OnRemovedWeapon;

    public WeaponManager activeWeapon;

    private Vector2 StickPos;

    void Start()
    {
        ActiveWeaponIndex = 0;
        
    }

    void Update()
    {
        activeWeapon = GetActiveWeapon();
        
        StickPos.x = fireStick.Horizontal;
        StickPos.y = fireStick.Vertical;

        if(StickPos.x != 0 && StickPos.y != 0)
        {
            float angle = Mathf.Atan2(StickPos.y, StickPos.x);
            activeWeapon.HandleShoot(StickPos);
        }

    }

    public WeaponManager GetActiveWeapon()
    {
        return m_WeaponSlots[ActiveWeaponIndex];
    }

}
