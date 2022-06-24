using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage:MonoBehaviour
{
    public float damage = 30f;

    private int timeTouched = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Damageable body = collision.gameObject.GetComponent<Damageable>();
        body.InflictDamage(damage, false, collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Damageable body = collision.gameObject.GetComponent<Damageable>();
        body.InflictDamage(damage*timeTouched/1000, false, collision.gameObject);

        timeTouched++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        timeTouched = 0;
    }


}
