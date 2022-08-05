using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private float shootingSpeed, minCooldown, maxCooldown, cooldown, chaseLength;
    public GameObject[] ballShoots;
    private Rigidbody2D ballShootsRb;
    private Ball ballScript;
    private float lastShoot;
    public Boss bossScript;

    private void FixedUpdate()
    {
        cooldown = Random.Range(minCooldown, maxCooldown);
        //is player in range?
        if (Vector3.Distance(bossScript.playerTransform.position, bossScript.startingPosition) < chaseLength)
        {
            if (Time.time - lastShoot > cooldown)
            {
                int index = Random.Range(0, ballShoots.Length);
                ballShootsRb = ballShoots[index].GetComponent<Rigidbody2D>();
                ballScript = ballShoots[index].GetComponent<Ball>();
                if (!ballScript.isShoot)
                {
                    lastShoot = Time.time;
                    ShootingBall(index);
                }
            }
        }
    }

    //shooting function
    private void ShootingBall(int index)
    {
        ballShootsRb.AddForce((bossScript.playerTransform.position - ballShoots[index].transform.position).normalized * shootingSpeed, ForceMode2D.Impulse);
        ballScript.SetIsShoot(true);
    }
}
