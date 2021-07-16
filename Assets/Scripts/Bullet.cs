using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("General")]
    [SerializeField] float rotateSpeed = 180f;
    [SerializeField] float damage = 20f;

    // Update is called once per frame
    void Update()
    {
        Rotate();
        DetectEdges();
    }

    private void Rotate()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void DetectEdges()
    {
        if (
                    transform.position.y > Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y ||
                    transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y ||
                    transform.position.x > Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x ||
                    transform.position.x < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x
                )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Health collisionHealth = collision.GetComponent<Health>();

        if (player)
            player.HitWithProjectile();

        Destroy(gameObject);

        if (!collisionHealth) return;

        collisionHealth.DealDamage(damage);
    }
}
