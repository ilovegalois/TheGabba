using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    [Header("Information")]    
    [Tooltip("The name that will be displayed in the UI for this weapon")]
    public string WeaponName;

    [Tooltip("The image that will be displayed in the UI for this weapon")]
    public Sprite WeaponIcon;

    [Tooltip("Tip of the weapon, where the projectiles are shot(FirePoint)")]
    public Transform WeaponMuzzle;

    [Tooltip("Goli hai bc")]
    public GameObject projectile;

    [Tooltip("Speed at which bullet will travel")]
    [Range(1f, 6f)]
    public float bulletSpeed = 2f;

    [Tooltip("Dealy of Shoot in seconds")]
    public float bulletDelay = 1f;
    float nextShot = float.NegativeInfinity;

    [Tooltip("Angle for the cone in which the bullets will be shot randomly (0 means no spread at all)")]
    public float BulletSpreadAngle = 0f;

    [Tooltip("Amount of bullets per shot")]
    public int BulletsPerShot = 1;

    [Tooltip("Ratio of the default FOV that this weapon applies while aiming")]
    [Range(0f, 1f)]
    public float AimZoomRatio = 1f;

    [Tooltip("Maximum number of shell that can be spawned before reuse")]
    [Range(1, 30)] public int ShellPoolSize = 1;

    [Tooltip("sound played when shooting")]
    public AudioClip ShootSfx;

    [Tooltip("Sound played when changing to this weapon")]
    public AudioClip ChangeWeaponSfx;

    public UnityAction OnShoot;
    public event Action OnShootProcessed;

    public GameObject Owner { get; set; }
    public GameObject SourcePrefab { get; set; }
    public bool IsWeaponActive { get; private set; }


    public void tryShoot(Vector2 direction, GameObject owner)
    {
        //I don't know why i did this.
        if (nextShot > Time.time + 6)
        {
            nextShot = Time.time;
        }

        if (Time.time > nextShot)
        {
            HandleShoot(direction, owner);

        }
    }
    private void HandleShoot(Vector2 direction, GameObject owner)
    {
        GameObject bullet = Instantiate(projectile, WeaponMuzzle.position, WeaponMuzzle.rotation);
        bullet.GetComponent<BulletProperties>().owner = owner;
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), owner.GetComponent<Collider2D>());
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

        nextShot = bulletDelay + Time.time;
    }
}
