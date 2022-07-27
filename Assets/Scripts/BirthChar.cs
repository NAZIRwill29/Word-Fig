using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthChar : MonoBehaviour
{
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

    // transfer char to keyboard
    public void CharToKeyboard()
    {
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

            }
        }

    }

    public void MinusCharNoInKeyboard()
    {
        totalCharInKeyboard -= 1;
        // Debug.Log(totalCharInKeyboard);
    }
}
