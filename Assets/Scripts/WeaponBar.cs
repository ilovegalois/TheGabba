using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponBar : MonoBehaviour
{
    public Image weaponImage;

    PlayerWeaponManager playerManager;

    private void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeaponManager>();

    }
    // Update is called once per frame
    void Update()
    {
        WeaponManager weaponManager = playerManager.GetActiveWeapon();
        if(weaponManager!=null) weaponImage.sprite = weaponManager.WeaponIcon;
    }
}
