using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIBoot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (collision.GetComponent<Player>().GetMovementSpeed() < 9f)
                collision.GetComponent<Player>().AddMovementSpeed(0.5f);

            Destroy(gameObject);
        }
    }
}
