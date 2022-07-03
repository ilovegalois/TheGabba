using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPickup : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Actor act = collision.gameObject.GetComponent<Actor>();
        if (act != null)
        {
            if (act.Affiliation == 0)
            {
                RoundManager.roundCash++;
                Destroy(this.gameObject);

            }
        }
    }
}
