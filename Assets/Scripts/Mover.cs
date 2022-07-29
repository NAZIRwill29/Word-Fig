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
    public AudioClip triggerSound;
    protected AudioSource moverAudio;
    protected float lastImmuneMover;
    private float rotationZ = 0;
    private float prevRotationZ;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originalSize = transform.localScale;
        moverAudio = GetComponent<AudioSource>();
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
    //update motor with target
    protected virtual void UpdateMotor(Vector3 input, GameObject targetObj)
    {
        float xSpeedTemp = 0;
        float ySpeedTemp = 0;
        float restrictMove = .2f;
        //restrict move
        if (input.x >= restrictMove)
            xSpeedTemp = xSpeed;
        else if (input.x <= -restrictMove)
            xSpeedTemp = xSpeed;
        else if (input.x < restrictMove && input.x > -restrictMove)
            xSpeedTemp = 0;

        if (input.y >= restrictMove)
            ySpeedTemp = ySpeed;
        else if (input.y <= -restrictMove)
            ySpeedTemp = ySpeed;
        else if (input.y < restrictMove && input.y > -restrictMove)
            ySpeedTemp = 0;
        //for reference move player
        moveDelta = new Vector3(input.x * xSpeedTemp, input.y * ySpeedTemp, 0);


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
        //Quaternion target = Quaternion.Euler(0, 0, GetAngleRotation(input.x, input.y));
        // Debug.Log(GetAngleRotation(transform.position.x, transform.position.y));
        prevRotationZ = rotationZ;
        rotationZ = GetAngleRotation(input.x, input.y);
        //Quaternion target = Quaternion.Euler(0, 0, rotationZ);
        //move target object
        // targetObj.transform.rotation = Quaternion.Slerp(targetObj.transform.rotation, target, 1);
        targetObj.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    //change dist x,y to angle rotation
    private float GetAngleRotation(float x, float y)
    {
        float angle;
        angle = Mathf.Atan(x / y) * (180 / Mathf.PI);
        //Debug.Log("angle =" + angle);
        if (x > 0 && y > 0)
            return 360 - angle;
        else if (x == 1 && y == 0)
            return 270;
        else if (x > 0 && y < 0)
            return 360 - (180 + angle);
        else if (x == 0 && y == -1)
            return 180;
        else if (x < 0 && y < 0)
            return 360 - (180 + angle);
        else if (x == -1 && y == 0)
            return 90;
        else if (x < 0 && y > 0)
            return 360 - (360 + angle);
        else if (x == 0 && y == 1)
            return 0;
        return prevRotationZ;
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmuneMover > immuneTime)
        {
            lastImmuneMover = Time.time;
            //make sound effect when collide
            moverAudio.PlayOneShot(triggerSound, 1.0f);
            //Debug.Log("play sound");
        }
        base.ReceiveDamage(dmg);


    }
}
