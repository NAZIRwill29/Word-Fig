using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharObj : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody2D charRb;
    private SpriteRenderer charSR;
    public CanvasGroup keyboardCG;
    // Start is called before the first frame update
    void Start()
    {
        charRb = GetComponent<Rigidbody2D>();
        charSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //shoot word
    public void ShootWord(GameObject targetChar)
    {
        //turn on SpriteRendere
        charSR.enabled = true;
        charRb.AddForce((targetChar.transform.position - transform.position).normalized * speed, ForceMode2D.Impulse);
    }

    //method trigger when collide
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fighter")
        {
            if (other.name != "Player")
            {
                charSR.enabled = false;
                Debug.Log("trigger");
                charRb.velocity = Vector2.zero;
                gameObject.transform.position = GameManager.instance.player.transform.position;
                keyboardCG.interactable = true;
            }
        }
    }
}
