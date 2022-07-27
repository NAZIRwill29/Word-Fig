using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempChar : MonoBehaviour
{
    //add char to word
    public void CharAddToWord(char letter)
    {
        Char charScript = gameObject.GetComponentInChildren<Char>();
        charScript.AddToWord(letter);
    }

    //delete word if not collide with enemy
    public void CharAddToBirth()
    {
        Char charScript = gameObject.GetComponentInChildren<Char>();
        charScript.AddToBirthchar(false);
    }
}
