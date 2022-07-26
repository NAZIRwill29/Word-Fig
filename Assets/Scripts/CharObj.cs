using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharObj : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody2D charRb;
    private SpriteRenderer charSR;
    public GameObject keyboard;
    private GameObject tempChar;
    public char letter;
    // Start is called before the first frame update
    void Start()
    {
        charRb = GetComponent<Rigidbody2D>();
        charSR = GetComponent<SpriteRenderer>();
        tempChar = GameObject.Find("TempChar");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //shoot word
    public void ShootWord(GameObject targetChar, char letterClick)
    {
        //get letter from char
        letter = letterClick;
        Debug.Log("LetterCharObj = " + letter);
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
                AfterCollide();
                //move char to word
                tempChar.GetComponent<TempChar>().AddToWord(letter);
            }
        }
        else if (other.tag == "Weapon")
        {
            return;
        }
        else
        {
            Debug.Log("none");
            AfterCollide();
            //delete char in temp
            tempChar.GetComponent<TempChar>().DeleteChar();
        }
    }

    private void AfterCollide()
    {
        //hide charObj
        charSR.enabled = false;
        Debug.Log("trigger");
        //stop force
        charRb.velocity = Vector2.zero;
        //make to original posistion
        gameObject.transform.position = GameManager.instance.player.transform.position;
        //enable keyboard interaction
        keyboard.GetComponent<CanvasGroup>().interactable = true;
    }
}
