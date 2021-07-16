using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Pathfinding;

public class Egg : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float launchTime = 2f;
    [SerializeField] float idleTime = 2f;
    [SerializeField] float prepareTime = 2f;

    Transform target;
    AIPath aiPath;

    bool canMove = true;
    bool preparing = false;

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

        target = FindObjectOfType<Player>().GetComponent<Transform>();

        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        if (aiDestinationSetter) aiDestinationSetter.target = target;

        StartCoroutine(dashCycle());
    }

    IEnumerator dashCycle()
    {
        float nextDelay;
 
        while (true)
        {
            if (!canMove && !preparing)
            {
                myRigidbody.constraints = 
                    RigidbodyConstraints2D.FreezePositionX |
                    RigidbodyConstraints2D.FreezePositionY |
                    RigidbodyConstraints2D.FreezeRotation;

                preparing = true;
                nextDelay = idleTime;
            }
            else if (!canMove && preparing)
            {
                canMove = true;
                preparing = false;
                nextDelay = prepareTime;
            }
            else
            {
                myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

                canMove = false;
                preparing = false;
                nextDelay = launchTime;
            }

            yield return new WaitForSeconds(nextDelay);

            if (!canMove && !preparing) GetComponent<EggShooter>().Shoot();

            myAnimator.SetTrigger("nextPhase");
        }    
    }
}
