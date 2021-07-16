using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorItem : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    [SerializeField] int dropChance = 25; // Out of 100

    // Start is called before the first frame update
    IEnumerator Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(Random.Range(0.2f, 0.6f));

        Freeze();
    }

    private void Freeze()
    {
        myRigidbody.constraints =
            RigidbodyConstraints2D.FreezePositionX |
            RigidbodyConstraints2D.FreezePositionY |
            RigidbodyConstraints2D.FreezeRotation;
    }

    public int GetDropChance()
    {
        return dropChance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Freeze();
        }
    }
}
