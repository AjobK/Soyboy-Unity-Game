using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;

    Transform target;
    AIPath aiPath;

    // Cached references
    private Animator myAnimator;
    private Rigidbody2D myRigidbody;
    private AIDestinationSetter aiDestinationSetter;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        aiPath = GetComponent<AIPath>();

        if (!FindObjectOfType<Player>()) return;

        target = FindObjectOfType<Player>().GetComponent<Transform>();

        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        if (aiDestinationSetter) aiDestinationSetter.target = target;
    }

    // Update is called once per frame
    void Update()
    {   
        SetMovementAnimation();
    }

    private void SetMovementAnimation()
    {
        myAnimator.SetFloat("lastVertical", -Math.Sign(aiPath.desiredVelocity.y));
        myAnimator.SetBool("isRunning", aiPath.desiredVelocity.x != 0 || aiPath.desiredVelocity.y != 0);
    }
}
