using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponButton : MonoBehaviour
{
    PlayerWeaponManager playerManager;
    void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeaponManager>();

    }
    public void OnSwitchWeapon()
    {
        playerManager.GetActiveWeapon().gameObject.SetActive(false);
        playerManager.SwitchWeapon(true);
    }
}
