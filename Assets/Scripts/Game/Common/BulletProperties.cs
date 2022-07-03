using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperties : MonoBehaviour
{
    public float lifeTime = 5f;
    public float damageInfliction = 10f;
    public bool explosiveAmmo = false;

    public GameObject owner;

    Animator anim;
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        Destroy(gameObject, lifeTime);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetBool("Explode", true);
        Damageable dmgBody = collision.gameObject.GetComponent<Damageable>();
        if (dmgBody != null)
        {
            dmgBody.InflictDamage(damageInfliction, false, owner);
        }
        Destroy(gameObject, 0.16f);

    }
}
