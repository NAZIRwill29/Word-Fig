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
    public Keyboard keyboard;
    private GameObject targetChar;
    private AudioSource wordAudio;
    public AudioClip triggerSound;
    public Player player;
    public CharObj charObj;
    private int damage;
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
        //Debug.Log(charWords.Length);
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
            //shoot word
            charObj.charAnim.SetTrigger("show");
            charObj.ShootWord(targetChar, damage);
            wordAudio.PlayOneShot(triggerSound, 1.0f);
            //increase mana and hp by refer on letter in word
            player.Heal(charWords.Length);
        }
        //RefreshTotalCharInWord();
    }

    //TODO - set new menu and interface

    //check if word exist
    private bool CheckWordExist(string word)
    {
        return words.Contains(word);
    }

    //change number of char in word - for limit
    public void ChangeTotalCharInWord(int number)
    {
        totalCharInWord += number;
        // Debug.Log(totalCharInWord);
    }

    //refresh number of char in word - for limit
    // public void RefreshTotalCharInWord()
    // {
    //     totalCharInWord = 0;
    //     // Debug.Log(totalCharInWord);
    // }
}
