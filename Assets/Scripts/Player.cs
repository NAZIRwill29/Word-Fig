using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    // Update is called once per frame
    void FixedUpdate()
    {
        //get input from user
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //call update motor in mover
        UpdateMotor(new Vector3(x, y, 0));
    }
}
