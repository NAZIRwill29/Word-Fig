using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    //Damage
    public int damage = 1;
    public float pushForce = 3;
    protected override void OnCollide(Collider2D coll)
    {
        //check if collide with player
        if (coll.tag == "Fighter" && coll.name == "Player")
        {
            //Debug.Log("damage");
            //Create new dmg obj b4 send to player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            //send message
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
