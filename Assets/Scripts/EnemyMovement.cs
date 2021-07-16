using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody2D;
    CapsuleCollider2D myBody;
    BoxCollider2D myGroundCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBody = GetComponent<CapsuleCollider2D>();
        myGroundCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FlipSprite();
    }

    private void Move()
    {
        myRigidbody2D.velocity = new Vector2(moveSpeed, 0);
    }

    private void FlipSprite()
    {
        myBody.transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Flips movement direction and sprite
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            moveSpeed *= -1;
        }
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
