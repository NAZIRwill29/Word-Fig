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
    private int damage;
    private float pushForce = 1;
    private AudioSource charObjAudio;
    public AudioClip triggerSound;
    public Animator charAnim;
    private bool isWordCombine;
    // Start is called before the first frame update
    void Start()
    {
        charRb = GetComponent<Rigidbody2D>();
        charSR = GetComponent<SpriteRenderer>();
        tempChar = GameObject.Find("TempChar");
        charObjAudio = GetComponent<AudioSource>();
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
        // Debug.Log("LetterCharObj = " + letter);
        //turn on SpriteRendere
        charSR.enabled = true;
        charRb.AddForce((targetChar.transform.position - transform.position).normalized * speed, ForceMode2D.Impulse);
        charObjAudio.PlayOneShot(triggerSound, 1.0f);
    }

    //shoot combine word
    public void ShootWord(GameObject targetChar, int damageWord)
    {
        //turn on SpriteRendere
        charSR.enabled = true;
        isWordCombine = true;
        // Debug.Log(isWordCombine);
        damage = damageWord;
        charRb.AddForce((targetChar.transform.position - transform.position).normalized * speed, ForceMode2D.Impulse);
        charObjAudio.PlayOneShot(triggerSound, 1.0f);
    }

    //method trigger when collide
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fighter")
        {
            if (other.name != "Player")
            {
                if (isWordCombine)
                {
                    // Debug.Log("wordcombine");
                    AfterCollide(other, damage);
                }
                else
                {
                    AfterCollide(other, 1);
                    //move char to word
                    tempChar.GetComponent<TempChar>().CharAddToWord(letter);
                }
            }
        }
        else if (other.tag == "Destructable")
        {
            if (isWordCombine)
            {
                AfterCollide(other, damage);
            }
            else
            {
                AfterCollide(other, 1);
                tempChar.GetComponent<TempChar>().CharAddToBirth();
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
            tempChar.GetComponent<TempChar>().CharAddToBirth();
        }
    }

    //after collide without damage
    private void AfterCollide()
    {
        //hide charObj
        charSR.enabled = false;
        // Debug.Log("trigger");
        //stop force
        charRb.velocity = Vector2.zero;
        //make to original posistion
        gameObject.transform.position = GameManager.instance.player.transform.position;
        charAnim.SetTrigger("hide");
        //enable keyboard interaction
        keyboard.GetComponent<CanvasGroup>().interactable = true;
        if (isWordCombine)
            isWordCombine = false;
    }
    //after collide with damage
    private void AfterCollide(Collider2D other, int damage)
    {
        // Debug.Log(damage);
        //Create new dmg obj b4 send to enemy
        Damage dmg = new Damage
        {
            damageAmount = damage,
            origin = transform.position,
            pushForce = pushForce
        };
        //send message to other to make call ReceiveDamage function
        other.SendMessage("ReceiveDamage", dmg);
        //hide charObj
        charSR.enabled = false;
        // Debug.Log("trigger");
        //stop force
        charRb.velocity = Vector2.zero;
        //make to original posistion
        gameObject.transform.position = GameManager.instance.player.transform.position;
        charAnim.SetTrigger("hide");
        //enable keyboard interaction
        keyboard.GetComponent<CanvasGroup>().interactable = true;
        if (isWordCombine)
            isWordCombine = false;
    }
}
