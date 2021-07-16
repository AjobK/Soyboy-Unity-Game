using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIHeart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() && collision.GetComponent<Health>())
        {
            if (collision.GetComponent<Health>().GetMaxHealth() < 16)
                collision.GetComponent<Health>().AddMaxHealth(1);

            Destroy(gameObject);
        }
    }
}
