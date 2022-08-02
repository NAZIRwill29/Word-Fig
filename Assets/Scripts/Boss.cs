using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private float minCooldown, maxCooldown, cooldown, moveTriggerLength, moveX, moveY;
    private float lastMove, moveCount;
    // private bool isCanMove = true;
    protected override void Start()
    {
        base.Start();
    }
    // private void Update()
    // {
    //     if (isCanMove)
    //     {
    //         if (moveCount < 1000)
    //         {
    //             //Debug.Log("move");
    //             UpdateMotor(new Vector3(moveX, moveY, 0));
    //             moveCount++;
    //         }
    //         else
    //             StopMove();
    //     }
    // }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        // if (isCanMove)
        // {
        //     cooldown = Random.Range(minCooldown, maxCooldown);
        //     //is player in range?
        //     if (Vector3.Distance(playerTransform.position, startingPosition) < moveTriggerLength)
        //     {
        //         if (Time.time - lastMove > cooldown)
        //         {
        //             lastMove = Time.time;
        //             // float inputX = Random.Range(-1, 1);
        //             // float inputY = Random.Range(-1, 1);
        //             // Move(inputX, inputY);

        //             ChasePlayer();
        //         }
        //     }
        // }
    }

    ////////////////////////////////////////////////////////////////////////////////
    //chase Player
    private void ChasePlayer()
    {
        if (moveCount < 20)
        {
            Debug.Log("move");
            chaseLength = 4;
            triggerLength = 3;
            moveCount++;
        }
        else
        {
            StopChase();
        }
    }

    private void StopChase()
    {
        Debug.Log("stop");
        chaseLength = 0;
        triggerLength = 0;
        // isCanMove = false;
        moveCount--;
        if (moveCount < 0)
        {
            // isCanMove = true;
            moveCount = 0;
            return;
        }
        StopChase();
    }

    //move
    private void Move(float inputX, float inputY)
    {
        // isCanMove = true;
        moveX = inputX * 3;
        moveY = inputY * 3;
    }

    //stop move
    private void StopMove()
    {
        Debug.Log("stop");
        // isCanMove = false;
        moveCount = 0;
    }
}
