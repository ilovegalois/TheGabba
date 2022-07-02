using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage:MonoBehaviour
{
    public float damage = 30f;

    private int timeTouched = 0;
    private Damageable body;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        body = collision.gameObject.GetComponent<Damageable>();
        if(body != null)
        {

            body.InflictDamage(damage, false, collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        body = collision.gameObject.GetComponent<Damageable>();
        if (body != null)
        {
            body.InflictDamage(damage * timeTouched / 1000, false, this.gameObject);
            timeTouched++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (body == null)
        { 
        timeTouched = 0;
        }
    }

}
