//This script makes it so that if a ball touches the floor, it will despawn after 3 seconds (attached to each ball)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    public float despawnDelay = 3f;

    private bool hasTouchedFloor = false;

    void Update()
    {
        if (hasTouchedFloor)
        {
            Destroy(gameObject, despawnDelay);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasTouchedFloor && collision.gameObject.name == "Floor")
        {
            hasTouchedFloor = true;
        }
    }

}

