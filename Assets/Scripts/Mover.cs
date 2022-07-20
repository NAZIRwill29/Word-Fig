using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract = it has to be inherited
public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    public float ySpeed = 0.7f;
    public float xSpeed = 1.0f;
    private Vector3 originalSize;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originalSize = transform.localScale;
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        //for reference move player
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //make player face direction it walk
        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);

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
