using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    [Tooltip("scene for next scene")]
    public string[] sceneNames;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            //teleport player- move new scene
            SceneManager.LoadScene(sceneName);
            //save game
            GameManager.instance.SaveState();
        }
    }
}
