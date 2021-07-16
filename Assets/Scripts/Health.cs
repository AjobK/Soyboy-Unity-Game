using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("General")]
    [SerializeField] float maxHP = 3f;
    [Range(0f, 1f)] [SerializeField] float startHPMultiplier = 1f;
    [SerializeField] bool immune = false;

    [Header("Passive Healing")]
    [SerializeField] float passiveHealingAmount = 0f;
    [SerializeField] float passiveHealingDelay = 2f;    // In seconds bulletPrefab;
    Coroutine passiveHealingRoutine;

    [SerializeField] float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = (int) (maxHP * startHPMultiplier);
    }

    public float DealDamage(float damage)
    {
        if (immune) return 0f;

        currentHP -= damage;

        if (currentHP <= 0)
        {
            if (GetComponent<Player>())
                GetComponent<Player>().Die();
            else
                Destroy(gameObject);

            if (GetComponent<LootSpawner>())
                GetComponent<LootSpawner>().SpawnLoot();
        } 
        else if (currentHP > 0 && passiveHealingAmount > 0 && passiveHealingRoutine == null)
        {
            passiveHealingRoutine = StartCoroutine(HealPassively());
        }

        // Return current HP to know whether entity is dead
        return currentHP;
    }

    public void Heal(float amount)
    {
        if (currentHP > 0 && amount > 0)
        {
            // Never healing more than 'amount'
            if (amount > maxHP - currentHP) amount = maxHP - currentHP;

            currentHP += amount;
        }
    }

    IEnumerator HealPassively()
    {
        while (true)
        {
            Heal(passiveHealingAmount);

            // Stops passive healing when full HP
            if (currentHP >= maxHP && passiveHealingRoutine != null) StopCoroutine(passiveHealingRoutine);

            yield return new WaitForSeconds(passiveHealingDelay);
        }
    }

    public float GetMaxHealth()
    {
        return maxHP;
    }

    public void SetMaxHealth(int amount)
    {
        maxHP = amount;
        Heal(amount);
    }

    public void AddMaxHealth(int amount)
    {
        maxHP += amount;
        Heal(amount);
    }

    public float GetCurrentHealth()
    {
        return currentHP;
    }
}
