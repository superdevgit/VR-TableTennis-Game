/*
 * This script makes the controller vibrate upon contact with the ball.
 * The strength of the vibration is determined using the relative velocities of the bat and the ball 
 * (meaning a "fast collision" should cause a stronger vibration)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerVibration : MonoBehaviour
{
    public float maxVelocity = 1f;
    private GameObject colliderObject;

    private void Update()
    {
        if (colliderObject)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Rigidbody otherRB = colliderObject.GetComponent<Rigidbody>();

            float velocity = 0f;
            //if the other objects stays on the bat, you will still be able to feel it rolling around on your bat (because your controller vibrates)
            if (otherRB)
            {
                velocity = rb.velocity.magnitude + otherRB.velocity.magnitude;
            }
            else
            {
                velocity = rb.velocity.magnitude;
            }

            float amplitude = Mathf.Min(1f, velocity / maxVelocity);

            OVRInput.SetControllerVibration(amplitude, amplitude, OVRInput.Controller.RTouch);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        colliderObject = col.gameObject;

        Rigidbody rb = GetComponent<Rigidbody>();
        Rigidbody otherRB = col.gameObject.GetComponent<Rigidbody>();

        float velocity = 0f;

        //compute relative velocity of objects to set vibration intensity
        if (otherRB)
        {
            velocity = rb.velocity.magnitude + otherRB.velocity.magnitude;
        }
        else
        {
            velocity = rb.velocity.magnitude;
        }

        float amplitude = Mathf.Min(1f, velocity / maxVelocity);

        OVRInput.SetControllerVibration(1f, amplitude, OVRInput.Controller.RTouch);
       
    }

    private void OnCollisionExit(Collision collision)
    {
        //stop vibration if the ball leaves collider
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch);
        colliderObject = null;
    }
}

