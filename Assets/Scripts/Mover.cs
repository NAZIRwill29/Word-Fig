using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract = it has to be inherited
public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.7f;
    protected float xSpeed = 1.0f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        //for reference move player
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //make player face direction it walk
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        //add push vector, if any
        moveDelta += pushDirection;
        //Reduce pushForce evert frame, based off receovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //prevent player overlap with NPC and Wall - block player
        //for make hit y
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        //check if hit any object
        if (hit.collider == null)
        {
            //move player in y
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        //for make hit x
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        //check if hit any object
        if (hit.collider == null)
        {
            //move player in x
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
