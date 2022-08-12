using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private float minCooldown, maxCooldown, cooldown, moveTriggerLength, moveX, moveY;
    private float lastMove, moveCount;
    //isBossSpecialWord - for boss only damaged with word only
    [SerializeField]
    private bool isEndStage, isBossSpecialWord;
    public InGame inGameScript;
    [SerializeField]
    private TextAsset wordList;
    private List<string> words;
    // private bool isCanMove = true;
    private void Awake()
    {
        if (isBossSpecialWord)
        {
            //convert word in .txt to string word
            words = new List<string>(wordList.text.Split(new char[]{
            ',', ' ', '\n', '\r'},
                System.StringSplitOptions.RemoveEmptyEntries
            ));
        }

    }
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

    //make only take damage when word is exist in it library
    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isBossSpecialWord)
        {
            base.ReceiveDamage(dmg);
        }
        else
        {
            //check if word exist then can take damage
            if (!CheckWordExist(charObjScript.word))
                return;
            if (Time.time - lastImmuneMover > immuneTime)
            {
                lastImmuneMover = Time.time;
                //make sound effect when collide
                moverAudio.PlayOneShot(triggerSound, 1.0f);
                //Debug.Log("play sound");

                hitpoint -= dmg.damageAmount;
                pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

                GameManager.instance.ShowText(dmg.damageAmount.ToString(), 15, Color.red, transform.position, Vector3.zero, 0.5f);

                //check to make death
                if (hitpoint <= 0)
                {
                    hitpoint = 0;
                    Death();
                }

            }
        }
    }

    //check if word exist
    private bool CheckWordExist(string word)
    {
        return words.Contains(word);
    }

    protected override void Death()
    {
        base.Death();
        if (isEndStage)
            inGameScript.PassStagePassed();

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
