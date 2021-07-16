using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterUI : MonoBehaviour
{
    // Cached reference
    Player player;
    Text myText;
    int currentCoins = -1;


    private void Awake()
    {
        if (FindObjectsOfType<CoinCounterUI>().Length > 1)
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
        player = FindObjectOfType<Player>();

        SetCoinCount();
    }

    // Update is called once per frame
    void Update()
    {
        SetCoinCount();
    }

    private void SetCoinCount()
    {
        if (currentCoins != player.GetCoinsAmount())
        {
            currentCoins = player.GetCoinsAmount();
            myText.text = currentCoins.ToString();
        }
    }
}
