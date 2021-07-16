using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Configuration
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] int keysAmount = 0;
    [SerializeField] int coinsAmount = 0;

    // State
    bool isAlive = true;
    float defaultGravity;
    float[] lastDirections = { 0f, -1f };
    bool gettingKnockedBack = false;
    bool victoryWalking = false;
    float xDiff;
    float yDiff;
    float knockbackTime;

    // Cached references
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private BoxCollider2D myCollider2D;
    private GameObject myBody;
    private GameManager gm;

    // Message then methods
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<BoxCollider2D>();
        myBody = transform.Find("Body").gameObject;

        gm = FindObjectOfType<GameManager>();

        if (gm)
        {
            GetComponent<Health>().SetMaxHealth(gm.GetHeartsAmount());
            GetComponent<PlayerShooter>().SetMaxAmmo(gm.GetAmmoAmount());
            coinsAmount = gm.GetCoinsAmount();
        }

        ResetCollision();
    }

    private void ResetCollision()
    {
        GetComponent<PlayerShooter>().SetCanShoot(true);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), false);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("EnemyProjectile"), false);
    }

    void Update()
    {
        if (!isAlive) return;

        if (victoryWalking)
        {
            VictoryWalk();

            return;
        }

        if (gettingKnockedBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + xDiff, transform.position.y + yDiff), movementSpeed * Time.deltaTime * 2);

            if (Time.time - knockbackTime > 0.2f)
            {
                gettingKnockedBack = false;
                knockbackTime = 0;
            }
        }

        if (!gettingKnockedBack) Run();
    }

    public float[] GetLastDirections()
    {
        return lastDirections;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    private void Run()
    {
        float horizontalMovement, verticalMovement;

        SetPlayerVelocity(out horizontalMovement, out verticalMovement);
        SetPlayerMovementAnimation(horizontalMovement, verticalMovement);
    }

    private void SetPlayerMovementAnimation(float horizontalMovement, float verticalMovement)
    {
        if (Math.Sign(horizontalMovement) != 0)
        {
            lastDirections = new float[] { Math.Sign(horizontalMovement), 0 };
            myAnimator.SetFloat("lastHorizontal", lastDirections[0]);
            myAnimator.SetFloat("lastVertical", lastDirections[1]);
        }

        if (Math.Sign(verticalMovement) != 0)
        {
            lastDirections = new float[] { 0, Math.Sign(verticalMovement) };
            myAnimator.SetFloat("lastHorizontal", lastDirections[0]);
            myAnimator.SetFloat("lastVertical", lastDirections[1]);
        }

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed || playerHasVerticalSpeed);
    }

    private void VictoryWalk()
    {
        SetPlayerMovementAnimation(0f, 1f);
        myAnimator.SetBool("isRunning", true);

        Vector2 playerVelocity = new Vector2(0f, 0.5f * movementSpeed);
        myRigidbody.velocity = playerVelocity;
    }

    private void SetPlayerVelocity(out float horizontalMovement, out float verticalMovement)
    {
        horizontalMovement = CrossPlatformInputManager.GetAxis("Horizontal");
        verticalMovement = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 playerVelocity = new Vector2(horizontalMovement * movementSpeed, verticalMovement * movementSpeed);
        myRigidbody.velocity = playerVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gettingKnockedBack) gettingKnockedBack = false;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && isAlive)
        {
            GetComponent<Health>().DealDamage(1f);

            xDiff = transform.position.x - collision.transform.position.x;
            yDiff = transform.position.y - collision.transform.position.y;

            gettingKnockedBack = true;
            StartCoroutine(SetTemporaryImmune());
            knockbackTime = Time.time;
        }
    }

    public void HitWithProjectile()
    {
        StartCoroutine(SetTemporaryImmune());
    }

    IEnumerator SetTemporaryImmune()
    {
        GetComponent<PlayerShooter>().SetCanShoot(false);
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"));
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("EnemyProjectile"));

        yield return new WaitForSeconds(2f);

        ResetCollision();
    }

    public void Die()
    {
        isAlive = false;
        LightPulsate lp = FindObjectOfType<LightPulsate>();

        if (lp) Destroy(lp.gameObject);

        if (gm) gm.GoToEndScreen();
    }

    public void AddKey()
    {
        keysAmount++;
    }

    public void UseKey()
    {
        keysAmount--;
    }

    public int GetKeysAmount()
    {
        return keysAmount;
    }

    public int GetCoinsAmount()
    {
        return coinsAmount;
    }

    public void AddCoin()
    {
        coinsAmount++;
    }

    public void UseCoins(int amount)
    {
        coinsAmount -= amount;
    }

    public void SetVictoryWalk(bool victoryWalking)
    {
        this.victoryWalking = victoryWalking;
    }

    public void AddMovementSpeed(float amount)
    {
        movementSpeed += amount;
    }
}
