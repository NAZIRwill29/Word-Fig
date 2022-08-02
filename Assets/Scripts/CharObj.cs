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
    private int damageCombine;
    [SerializeField]
    private float pushForce = 3;
    private AudioSource charObjAudio;
    public AudioClip triggerSound;
    public Animator charAnim;
    private bool isWordCombine;
    public Sprite[] charSprites;
    private
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

    //increase damage after level up
    public void SetDamage(int dmg)
    {
        damage = dmg;
        Debug.Log("damage = " + damage);
    }

    //shoot word
    public void ShootWord(GameObject targetChar, char letterClick, string specialText)
    {
        //get letter from char
        letter = letterClick;
        // Debug.Log("LetterCharObj = " + letter);
        //turn on SpriteRendere
        charSR.enabled = true;
        SetSprite(specialText);
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
        damageCombine = damageWord;
        Debug.Log("damage = " + damageCombine);
        charRb.AddForce((targetChar.transform.position - transform.position).normalized * speed, ForceMode2D.Impulse);
        charObjAudio.PlayOneShot(triggerSound, 1.0f);
        StartCoroutine("StopCharObj");
    }

    //stop charObj movement
    private IEnumerator StopCharObj()
    {
        yield return new WaitForSeconds(7);
        AfterCollide();
        tempChar.GetComponent<TempChar>().CharAddToBirth();
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
                    AfterCollide(other, damageCombine);
                }
                else
                {
                    AfterCollide(other, damage);
                    //move char to word
                    tempChar.GetComponent<TempChar>().CharAddToWord(letter);
                }
            }
        }
        else if (other.tag == "Destructable")
        {
            if (isWordCombine)
            {
                AfterCollide(other, damageCombine);
            }
            else
            {
                AfterCollide(other, damage);
                tempChar.GetComponent<TempChar>().CharAddToBirth();
            }

        }
        else if (other.tag == "Weapon")
        {
            Debug.Log("Weapon");
            AfterCollide();
            tempChar.GetComponent<TempChar>().CharAddToBirth();
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
        keyboard.GetComponent<CanvasGroup>().alpha = 1;
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
        keyboard.GetComponent<CanvasGroup>().alpha = 1;
        if (isWordCombine)
            isWordCombine = false;
    }

    //set sprite - when attack
    public void SetSprite(string text)
    {
        switch (text)
        {
            case "thunder":
                charSR.sprite = charSprites[1];
                break;
            case "ice":
                charSR.sprite = charSprites[2];
                break;
            case "fire":
                charSR.sprite = charSprites[3];
                break;
            case "wind":
                charSR.sprite = charSprites[4];
                break;
            default:
                charSR.sprite = charSprites[0];
                break;
        }
    }
}
