using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCircle : MonoBehaviour
{
    public Transform[] balls;
    public float distance = 0.25f;
    public float[] ballSpeeds = { 2.5f, -2.5f };

    // Update is called once per frame
    void Update()
    {
        //make ball circle
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * ballSpeeds[i]) * distance, Mathf.Sin(Time.time * ballSpeeds[i]) * distance, 0);
        }
    }
}
