using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter
{
    public AudioClip triggerSound;
    protected AudioSource crateAudio;
    private float lastImmuneCrate;

    private void Start()
    {
        crateAudio = GetComponent<AudioSource>();
    }
    protected override void Death()
    {
        Destroy(gameObject);
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmuneCrate > immuneTime)
        {
            lastImmuneCrate = Time.time;
            //make sound effect when collide
            crateAudio.PlayOneShot(triggerSound, 1.0f);
        }
        base.ReceiveDamage(dmg);

    }
}
