using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    [Tooltip("scene for next scene")]
    public string[] sceneNames;
    public string spawnPointName;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            //pass spawnPointName to gameManager
            GameManager.instance.PassSpawnPointName(spawnPointName);
            //teleport player- move new scene
            SceneManager.LoadScene(sceneName);
            //save game
            GameManager.instance.SaveState();
            //make sound effect when attack
            collidableAudio.PlayOneShot(triggerSound, 1.0f);
        }
    }
}