using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //damage struct
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
    public float[] pushForce = { 1.0f, 1.2f, 1.4f, 1.6f, 2.0f, 2.3f, 2.6f, 2.8f, 3.0f, 3.2f, 3.5f, 3.7f, 4.0f, 4.5f };
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
        if (Input.GetKeyDown(KeyCode.Space))
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
                Debug.Log(coll.name);
                //create new dmg obj, send to fighter
                Damage dmg = new Damage
                {
                    damageAmount = damagePoint[weaponLevel],
                    origin = transform.position,
                    pushForce = pushForce[weaponLevel]
                };
                //send message
                coll.SendMessage("ReceiveDamage", dmg);
            }
        }
    }
}
