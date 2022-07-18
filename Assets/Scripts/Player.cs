using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        //get input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //movement
        moveDelta = new Vector3(x, y, 0);
        //flip player face
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

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
