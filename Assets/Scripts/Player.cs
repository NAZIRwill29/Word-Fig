using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    public GameObject targetObj;
    private bool isAlive = true;
    public AudioClip healSound;
    public int manapoint = 20;
    public int maxManapoint = 20;
    private float lastIncreaseMana;
    private float cooldown = 5;
    public Joystick joystick;
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        if (manapoint < maxManapoint)
        {
            //check cooldown
            if (Time.time - lastIncreaseMana > cooldown)
            {
                lastIncreaseMana = Time.time;
                //increase mana
                ChangeMana(1);
                GameManager.instance.OnManapointChange();
            }
        }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //get input from user for move player
        // float x = Input.GetAxisRaw("Horizontal");
        // float y = Input.GetAxisRaw("Vertical");

        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        if (isAlive)
            //call update motor in mover
            UpdateMotor(new Vector3(x, y, 0), targetObj);
    }
    //swap character
    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }
    //when lvl up
    public void OnLevelUp()
    {
        int increasePoint = (int)(2 * (maxHitpoint / 10));
        //increase hp
        maxHitpoint += increasePoint;
        hitpoint = maxHitpoint;
        //increase mp
        maxManapoint += increasePoint;
        manapoint = maxManapoint;
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }
    //heal hp mp player
    public void Heal(int healingAmount)
    {
        //check if maxhealth
        if (hitpoint != maxHitpoint)
            hitpoint += healingAmount;
        if (manapoint != maxManapoint)
            manapoint += healingAmount;
        //prevent from more than maxhealth
        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;
        if (manapoint > maxManapoint)
            manapoint = maxManapoint;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        //change hp
        GameManager.instance.OnHitpointChange();
        GameManager.instance.OnManapointChange();
        //heal sound
        moverAudio.PlayOneShot(triggerSound, 1.0f);
    }

    //heal hp
    public void Heal(int healingAmount, bool isHp)
    {
        if (isHp)
        {
            //check if maxhealth
            if (hitpoint != maxHitpoint)
                hitpoint += healingAmount;
            //prevent from more than maxhealth
            if (hitpoint > maxHitpoint)
                hitpoint = maxHitpoint;
            //change hp
            GameManager.instance.OnHitpointChange();
        }
        else
        {
            if (manapoint != maxManapoint)
                manapoint += healingAmount;
            if (manapoint > maxManapoint)
                manapoint = maxManapoint;
            //change mp
            GameManager.instance.OnManapointChange();
        }
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        //heal sound
        moverAudio.PlayOneShot(triggerSound, 1.0f);
    }

    // when receive damage
    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
        //make sound effect when collide
        //playerAudio.PlayOneShot(triggerSound, 1.0f);
    }
    //death
    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }
    public void Respawn()
    {
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
    //change mana
    public void ChangeMana(int point)
    {
        manapoint += point;
    }
}
