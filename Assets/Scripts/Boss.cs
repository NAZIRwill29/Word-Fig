using UnityEngine;

public class Boss : Enemy
{
    public float distance = 0.25f;
    public float[] fireballSpeed = { 2.5f, -2.5f };
    public Transform[] fireballs;
    private void Update()
    {
        //make fireball circle
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * distance, Mathf.Sin(Time.time * fireballSpeed[i]) * distance, 0);
        }
    }
}
