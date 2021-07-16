using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounterUI : MonoBehaviour
{
    // Cached reference
    PlayerShooter playerShooter;
    Text myText;
    int currentAmmo;
    int maxAmmo;

    private void Awake()
    {
        if (FindObjectsOfType<AmmoCounterUI>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        playerShooter = FindObjectOfType<PlayerShooter>();

        SetAmmoCount();
    }

    // Update is called once per frame
    void Update()
    {
        SetAmmoCount();
    }

    private void SetAmmoCount()
    {
        if (currentAmmo != playerShooter.GetCurrentAmmo() || maxAmmo != playerShooter.GetMaxAmmo())
        {
            maxAmmo = playerShooter.GetMaxAmmo();
            currentAmmo = playerShooter.GetCurrentAmmo();
            myText.text = currentAmmo + " OF " + maxAmmo;
        }
    }
}
