﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIAmmo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerShooter>())
        {
            collision.GetComponent<PlayerShooter>().AddMaxAmmo(1);
            Destroy(gameObject);
        }
    }
}
