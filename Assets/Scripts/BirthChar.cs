using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthChar : MonoBehaviour
{
    public Player player;
    private Char[] charKeyboards;
    private int totalCharInKeyboard = 0;
    private float cooldown = 0.3f;
    private float lastSpawn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // transfer char to keyboard - button A
    public void CharToKeyboard()
    {
        //if none mana
        if (player.manapoint <= 0)
            return;
        //check cooldown
        if (Time.time - lastSpawn > cooldown)
        {
            lastSpawn = Time.time;
            if (totalCharInKeyboard < 16)
            {
                charKeyboards = GetComponentsInChildren<Char>();
                //spawn object under parent
                //Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)
                //Instantiate(charPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
                charKeyboards[0].AddToKeyboard();
                totalCharInKeyboard += 1;
                // Debug.Log(totalCharInKeyboard);
                //decrease mana
                player.ChangeMana(-1);
                GameManager.instance.OnManapointChange();
            }
        }

    }

    public void MinusCharNoInKeyboard()
    {
        totalCharInKeyboard -= 1;
        // Debug.Log(totalCharInKeyboard);
    }

    //set special char
    //level - 3 - thunder -> 7 - ice -> 10 - fire -> 15 - wind
    public void SetSpecialChar(int level)
    {
        charKeyboards = GetComponentsInChildren<Char>();
        if (level >= 3)
            charKeyboards[0].SetSpecialChar("thunder");
        else if (level >= 7)
            charKeyboards[2].SetSpecialChar("ice");
        else if (level >= 10)
            charKeyboards[4].SetSpecialChar("fire");
        else if (level >= 15)
            charKeyboards[6].SetSpecialChar("wind");
    }
}
