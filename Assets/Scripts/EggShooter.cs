using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float speed = 100f;

    public void Shoot()
    {
        // X and Y stored for bullets (x, y)
        float[][] bulletsData = new float[][] {
            new float[] {0f, -1f}, // Up
            new float[] {0f, 1f}, // Down
            new float[] {-1f, 0f}, // Left
            new float[] {1f, 0f}  // Right
        };

        for (int i = 0; i < bulletsData.Length; i++)
        {
            GameObject bullet = Instantiate(
                bulletPrefab,
                gameObject.transform.position + new Vector3(bulletsData[i][0], bulletsData[i][1], 0),
                Quaternion.identity
            ) as GameObject;

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
                Mathf.Floor(bulletsData[i][0]) * speed,
                Mathf.Floor(bulletsData[i][1]) * speed
            );
        }
    }
}
