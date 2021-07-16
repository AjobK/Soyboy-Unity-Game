using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    [SerializeField] GameObject[] vendorItemPrefabs;
    int coinAmount = 0;
    bool acceptingCoins = true;

    // Cached references
    Animator myAnimator;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() && player != null && player.GetCoinsAmount() >= 1 && acceptingCoins)
        {
            player.UseCoins(1);
            coinAmount++;

            myAnimator.SetInteger("coinAmount", coinAmount);

            if (coinAmount >= 3)
            {
                LaunchItem();
                StartCoroutine(RebootVendingMachine());

                acceptingCoins = false;
                coinAmount = 0;
            }
        }
    }

    IEnumerator RebootVendingMachine()
    {
        yield return new WaitForSeconds(1f);

        myAnimator.SetInteger("coinAmount", coinAmount);

        acceptingCoins = true;
    }

    private void LaunchItem()
    {
        int dropNumber = Random.Range(0, 100);
        int fromNumber = 0;
        int toNumber = 0;

        for (int i = 0; i < vendorItemPrefabs.Length; i++)
        {
            toNumber = fromNumber + vendorItemPrefabs[i].GetComponent<VendorItem>().GetDropChance();

            if ((fromNumber <= dropNumber && dropNumber <= toNumber) || i >= vendorItemPrefabs.Length - 1)
            {
                Transform launcherTransform = gameObject.transform.Find("Launcher");

                GameObject item = Instantiate(
                    vendorItemPrefabs[i],
                    launcherTransform.position,
                    Quaternion.identity
                ) as GameObject;

                item.GetComponent<Rigidbody2D>().velocity = new Vector2(
                    Random.Range(-1.5f, 1.5f),
                    -5f
                );

                return;
            }

            fromNumber = toNumber + 1;
        }
    }
}
