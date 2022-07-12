/*
 * This was used in an early version. There would be an object which would just spawn in balls every couple of seconds.
 * You can still use this by enabling it on the "IntervalBallSpawner" gamemode.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public float interval = 5f;
    public GameObject ball;
    private float timeSinceLastSpawn = 0f;

    void Start()
    {
        Instantiate(ball, transform.position, transform.rotation);
    }

    void FixedUpdate()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= interval)
        {
            //spawn ball after enough time has passed
            Instantiate(ball, transform.position, transform.rotation);
            timeSinceLastSpawn = 0f;
        }
    }
}
