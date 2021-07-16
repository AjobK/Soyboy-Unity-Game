using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUI : MonoBehaviour
{
    Player player;
    bool isShown = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        transform.GetChild(0).gameObject.SetActive(isShown);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShown && player && player.GetKeysAmount() > 0)
        {
            isShown = true;
            transform.GetChild(0).gameObject.SetActive(isShown);
        }
        else if (isShown && player && player.GetKeysAmount() <= 0)
        {
            isShown = false;
            transform.GetChild(0).gameObject.SetActive(isShown);
        }
    }
}
