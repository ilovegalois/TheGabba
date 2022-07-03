using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponManager : MonoBehaviour
{

    public Joystick fireStick;


    [Tooltip("Parent transform where all weapon will be added in the hierarchy")]
    public Transform WeaponParentSocket;
    public int ActiveWeaponIndex { get; private set; }
    public int ActiveWeaponCount { get; private set; }
    
    public List<WeaponManager> startingWeapon;

    public WeaponManager[] m_WeaponSlots = new WeaponManager[4]; // 4 weapon slots

    public UnityAction<WeaponManager> OnSwitchedToWeapon;
    public UnityAction<WeaponManager, int> OnAddedWeapon;
    public UnityAction<WeaponManager, int> OnRemovedWeapon;

    private WeaponManager activeWeapon;

    private Vector2 StickPos;
    private Animator anim;


    void Start()
    {
        ActiveWeaponIndex = 0;
        for (int i=0; i<=DataController.AllowedWeapons; i++)
        {
            AddWeapon(startingWeapon[i]);           
        }

        SwitchWeapon(true);
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        activeWeapon = GetActiveWeapon();
        
        StickPos.x = fireStick.Horizontal;
        StickPos.y = fireStick.Vertical;

        
        if (StickPos.x != 0 || StickPos.y != 0 && activeWeapon!=null)
        {
            AnimateFire();
            StickPos = EquateStick(StickPos);
            Shoot(StickPos);
        }
        else
        {
            anim.SetBool("Shooting", false);
        }
    }
    public void AnimateFire()
    {
        anim.SetBool("Shooting", true);

        anim.SetFloat("f_Hor", StickPos.x);
        anim.SetFloat("f_Vert", StickPos.y);
    }
    public Vector2 EquateStick(Vector2 initStick)
    {
        Vector2 retSticks;
        float x = initStick.x;
        float y = initStick.y;
        float cent = Math.Abs(x) + Math.Abs(y);
        retSticks = new Vector2(x/cent, y/cent);
        return retSticks;
    }

    public void Shoot(Vector2 shotDirect)
    {
        activeWeapon.tryShoot(shotDirect, this.gameObject);
    }

    public bool AddWeapon(WeaponManager weaponPrefab)
    {
        // if we already hold this weapon type (a weapon coming from the same source prefab), don't add the weapon
        if (HasWeapon(weaponPrefab) != null)
        {
            return false;
        }

        // search our weapon slots for the first free one, assign the weapon to it, and return true if we found one. Return false otherwise
        for (int i = 0; i < m_WeaponSlots.Length; i++)
        {
            // only add the weapon if the slot is free
            if (m_WeaponSlots[i] == null)
            {
                // spawn the weapon prefab as child of the weapon socket
                WeaponManager weaponInstance = Instantiate(weaponPrefab, WeaponParentSocket);
                weaponInstance.transform.localPosition = Vector2.zero;
                weaponInstance.transform.localRotation = Quaternion.identity;

                // Set owner to this gameObject so the weapon can alter projectile/damage logic accordingly
                weaponInstance.Owner = gameObject;
                weaponInstance.SourcePrefab = weaponPrefab.gameObject;



                m_WeaponSlots[i] = weaponInstance;

                if (OnAddedWeapon != null)
                {
                    OnAddedWeapon.Invoke(weaponInstance, i);
                }

                return true;
            }
        }

        // Handle auto-switching to weapon if no weapons currently
        if (GetActiveWeapon() == null)
        {
            SwitchWeapon(true);
        }

        return false;
    }

    public bool RemoveWeapon(WeaponManager weaponInstance)
    {
        // Look through our slots for that weapon
        for (int i = 0; i < m_WeaponSlots.Length; i++)
        {
            // when weapon found, remove it
            if (m_WeaponSlots[i] == weaponInstance)
            {
                m_WeaponSlots[i] = null;

                if (OnRemovedWeapon != null)
                {
                    OnRemovedWeapon.Invoke(weaponInstance, i);
                }

                Destroy(weaponInstance.gameObject);

                // Handle case of removing active weapon (switch to next weapon)
                if (i == ActiveWeaponIndex)
                {
                    SwitchWeapon(true);
                }

                return true;
            }
        }

        return false;
    }

    public WeaponManager GetActiveWeapon()
    {
        return m_WeaponSlots[ActiveWeaponIndex];
    }
    public WeaponManager HasWeapon(WeaponManager weaponPrefab)
    {
        // Checks if we already have a weapon coming from the specified prefab
        for (var index = 0; index < m_WeaponSlots.Length; index++)
        {
            var w = m_WeaponSlots[index];
            if (w != null && w.SourcePrefab == weaponPrefab.gameObject)
            {
                return w;
            }
        }

        return null;
    }
    public void SwitchWeapon(bool ascendingOrder)
    {
        int newWeaponIndex = -1;
        int closestSlotDistance = m_WeaponSlots.Length;
        for (int i = 0; i < m_WeaponSlots.Length; i++)
        {
            // If the weapon at this slot is valid, calculate its "distance" from the active slot index (either in ascending or descending order)
            // and select it if it's the closest distance yet
            if (i != ActiveWeaponIndex && GetWeaponAtSlotIndex(i) != null)
            {
                int distanceToActiveIndex = GetDistanceBetweenWeaponSlots(ActiveWeaponIndex, i, ascendingOrder);

                if (distanceToActiveIndex < closestSlotDistance)
                {
                    closestSlotDistance = distanceToActiveIndex;
                    newWeaponIndex = i;
                }
            }
        }
        if (newWeaponIndex == -1) newWeaponIndex = ActiveWeaponIndex;

        // Handle switching to the new weapon index
        SwitchToWeaponIndex(newWeaponIndex);
    }

    public WeaponManager GetWeaponAtSlotIndex(int index)
    {
        // find the active weapon in our weapon slots based on our active weapon index
        if (index >= 0 &&
            index < m_WeaponSlots.Length)
        {
            return m_WeaponSlots[index];
        }

        // if we didn't find a valid active weapon in our weapon slots, return null
        return null;
    }
    public void SwitchToWeaponIndex(int newWeaponIndex)
    {
        ActiveWeaponIndex = newWeaponIndex;
        if(!m_WeaponSlots[ActiveWeaponIndex].gameObject.activeSelf) m_WeaponSlots[ActiveWeaponIndex].gameObject.SetActive(true);
    }
    int GetDistanceBetweenWeaponSlots(int fromSlotIndex, int toSlotIndex, bool ascendingOrder)
    {
        int distanceBetweenSlots = 0;

        if (ascendingOrder)
        {
            distanceBetweenSlots = toSlotIndex - fromSlotIndex;
        }
        else
        {
            distanceBetweenSlots = -1 * (toSlotIndex - fromSlotIndex);
        }

        if (distanceBetweenSlots < 0)
        {
            distanceBetweenSlots = m_WeaponSlots.Length + distanceBetweenSlots;
        }

        return distanceBetweenSlots;
    }
}
