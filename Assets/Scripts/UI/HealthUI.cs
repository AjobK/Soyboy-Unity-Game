using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Sprite filledHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] GameObject[] playerHearts;
    [SerializeField] float distanceBetweenHearts = 80f;
    [SerializeField] int amountOfHeartsPerRow = 3;

    // Cached reference
    Health playerHealth;
    float currentHealth;

    private void Awake()
    {
        if (FindObjectsOfType<HealthUI>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //myText = GetComponent<Text>();
        playerHealth = FindObjectOfType<Player>().GetComponent<Health>();

        CreateHeartsUI();
        SetPlayerHealthUI();
    }

    private void CreateHeartsUI()
    {
        playerHearts = new GameObject[(int)playerHealth.GetMaxHealth()];

        for (int i = 0; i < playerHearts.Length; i++)
        {
            GameObject heartObject = new GameObject("Heart");
            heartObject.AddComponent(typeof(Image));
            heartObject.GetComponent<Image>().sprite = i <= playerHealth.GetCurrentHealth()-1 ? filledHeart : emptyHeart;
            heartObject.transform.parent = gameObject.transform;
            heartObject.transform.position = new Vector2(
                gameObject.transform.position.x + (i % amountOfHeartsPerRow) * distanceBetweenHearts,
                gameObject.transform.position.y - distanceBetweenHearts * Mathf.Ceil(i / amountOfHeartsPerRow)
            );
            heartObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

            playerHearts[i] = heartObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHearts.Length != playerHealth.GetMaxHealth())
            CreateHeartsUI();

        SetPlayerHealthUI();
    }

    private void SetPlayerHealthUI()
    {
        if (currentHealth != playerHealth.GetCurrentHealth())
        {
            currentHealth = playerHealth.GetCurrentHealth();

            for (int i = 0; i < playerHearts.Length; i++)
            {
                playerHearts[i].GetComponent<Image>().sprite = i <= currentHealth-1 ? filledHeart : emptyHeart;
            }
        }
    }
}