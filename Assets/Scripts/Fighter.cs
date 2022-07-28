using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public field
    public int hitpoint = 20;
    public int maxHitpoint = 20;
    public float pushRecoverySpeed = 0.2f;
    //Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;
    //push
    protected Vector3 pushDirection;

    //All fighter can receive Damage / die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
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
    //death
    protected virtual void Death()
    {

    }
}
