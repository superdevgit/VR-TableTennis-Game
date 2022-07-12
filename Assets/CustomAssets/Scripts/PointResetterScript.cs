/*
 * This script will simply reset the points if the ball hits the floor during the target hitting gamemode.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointResetterScript : MonoBehaviour
{

    private bool first_floor_contact = true;

    public GameObject targetSpawner;

    private void OnCollisionEnter(Collision collision)
    { //if the ball hits the floor, reset the point counter
        if (collision.collider.gameObject.name.Contains("Floor"))
        {
            if (first_floor_contact) 
            {
                targetSpawner.GetComponent<SpawnTargets>().resetCounter();
                first_floor_contact = false;
            }
        }
    }
}
