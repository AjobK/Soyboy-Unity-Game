using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] int maxAmmo = 5;
    [SerializeField] int currentAmmo = 5;
    [SerializeField] float reloadDelay = 1f;
    
    Coroutine reloadRoutine;
    bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        ShootInput();
    }

    private void ShootInput()
    {
        if (!canShoot) return;

        if (Input.GetButtonDown("Fire") && currentAmmo > 0)
        {
            if (reloadRoutine != null) StopCoroutine(reloadRoutine);
            Fire();
            reloadRoutine = StartCoroutine(ReloadPassively());
        }
    }

    public void Fire()
    {
            if (currentAmmo <= 0) return;

            float[] playerLastDirections = GetComponent<Player>().GetLastDirections();
            float playerSpeed = GetComponent<Player>().GetMovementSpeed();
            
            GameObject bullet = Instantiate(
                bulletPrefab,
                gameObject.transform.position + new Vector3(playerLastDirections[0], playerLastDirections[1], 0),
                Quaternion.identity
            ) as GameObject;

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
                playerLastDirections[0] * playerSpeed * 1.4f,
                playerLastDirections[1] * playerSpeed * 1.4f
            );

            currentAmmo--;
    }

    IEnumerator ReloadPassively()
    {
        while (true)
        {
            yield return new WaitForSeconds(reloadDelay);

            currentAmmo++;

            // Stops passive reloading when full ammo
            if (currentAmmo >= maxAmmo) StopCoroutine(reloadRoutine);
        }
    }

    public void SetCanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    public void SetMaxAmmo(int amount)
    {
        maxAmmo = amount;
    }

    public void AddMaxAmmo(int amount)
    {
        maxAmmo += amount;
    }
}
