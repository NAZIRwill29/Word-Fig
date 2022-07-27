using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{
    private Char[] charWords;
    private char[] letters;
    private string letterCombine;
    [SerializeField]
    private TextAsset wordList;
    private List<string> words;
    public int totalCharInWord;
    private int damage = 1;
    private float pushForce = 2;
    public Keyboard keyboard;
    private GameObject targetChar;
    private AudioSource wordAudio;
    public AudioClip triggerSound;
    void Awake()
    {
        //convert word in .txt to string word
        words = new List<string>(wordList.text.Split(new char[]{
            ',', ' ', '\n', '\r'},
            System.StringSplitOptions.RemoveEmptyEntries
        ));
        wordAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //convert char to word for damage enemy
    public void WordAttack()
    {
        //refresh data
        letters = new char[9];
        letterCombine = "";
        //get CHAR script from child
        charWords = GetComponentsInChildren<Char>();
        Debug.Log(charWords.Length);
        for (int i = 0; i < charWords.Length; i++)
        {
            //get letter from char
            letters[i] = charWords[i].letter;
            //combine letter to be word
            letterCombine += letters[i].ToString();
            charWords[i].AddToBirthchar(true);
        }
        Debug.Log("Word = " + letterCombine);
        Debug.Log(CheckWordExist(letterCombine));
        if (CheckWordExist(letterCombine))
        {
            //calculate damage based on letter in word
            damage = charWords.Length;
            //get target enemy from keyboard
            targetChar = keyboard.targetChar;
            //Create new dmg obj b4 send to enemy
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            //send message to other to make call ReceiveDamage function
            targetChar.SendMessage("ReceiveDamage", dmg);
            wordAudio.PlayOneShot(triggerSound, 1.0f);
        }
        //RefreshTotalCharInWord();
    }

    //TODO - set new menu and interface

    //check if word exist
    private bool CheckWordExist(string word)
    {
        return words.Contains(word);
    }

    //increase number of char in word - for limit
    public void IncreaseTotalCharInWord()
    {
        totalCharInWord += 1;
        // Debug.Log(totalCharInWord);
    }

    //decrease number of char in word - for limit
    public void DecreaseTotalCharInWord()
    {
        totalCharInWord -= 1;
        // Debug.Log(totalCharInWord);
    }

    //refresh number of char in word - for limit
    // public void RefreshTotalCharInWord()
    // {
    //     totalCharInWord = 0;
    //     // Debug.Log(totalCharInWord);
    // }
}
