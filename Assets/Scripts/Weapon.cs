using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //damage struct
    public int[] damagePoint = { 1, 2, 3 };
    public float[] pushForce = { 1.0f, 1.2f, 1.4f };
    //upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;
    private float cooldown = 0.3f;
    private float lastSwing;
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        //change weapon image
        // spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        // if (Input.GetKeyDown(KeyCode.Space))
        if (Input.GetButtonDown("Jump"))
        {
            //check cooldown
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                //swing weapon
                Swing();
            }
        }
    }

    //method swing weapon
    private void Swing()
    {
        anim.SetTrigger("Swing");
        //make sound effect when attack
        collidableAudio.PlayOneShot(triggerSound, 1.0f);
    }

    protected override void OnCollide(Collider2D coll)
    {
        //check tag obj
        if (coll.tag == "Fighter")
        {
            //check name obj
            if (coll.name != "Player")
            {
                //send message
                //Debug.Log(coll.name);
                //create new dmg obj, send to fighter
                Damage dmg = new Damage
                {
                    damageAmount = damagePoint[weaponLevel],
                    origin = transform.position,
                    pushForce = pushForce[weaponLevel]
                };
                //send message to call function ReceiveDamage in fighter.cs
                coll.SendMessage("ReceiveDamage", dmg);
            }
        }
    }
    //upgrade weapon
    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
        //change stats%
    }
}
