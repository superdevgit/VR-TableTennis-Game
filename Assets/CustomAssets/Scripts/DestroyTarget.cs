/*
 * This is used on the targets. If they get hit by a ball, both the ball and the target will despawn.
 * The point counter gets increased and then a new target will get spawned.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    private GameObject parent;

    void Start()
    {
        parent = transform.parent.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if a ball hits the target, destroy the ball and the target, spawn a new target and increase the counter
        if (collision.gameObject.name.Contains("Ball"))
        {
            SpawnTargets parentScript = parent.GetComponent<SpawnTargets>();
            parentScript.SpawnNewTarget();
            parentScript.AddHit();
            Destroy(collision.gameObject);
        }
    }
}
