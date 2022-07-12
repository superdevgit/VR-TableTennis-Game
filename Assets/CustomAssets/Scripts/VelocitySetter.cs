/*
 * This was an absolute nightmare to figure out. When using Oculus' implementation of the controller tracking, the objects will just "teleport" to the new
 * locations on every position update. The problem with this is that then the rigidbody never actually gets any speed/energy. This, in turn,
 * causes some really weird physics interaction between the bat and the ball to occur. The ball will regularly just "phase" through the bat
 * because it doesn't actually get any velocity/energy form the collision. 
 * 
 * The fix is that you just kind of interpolate between the positions and calculate the velocity/energy based on the distance between the old and the new position.
 * 
 * At some point I even tried to do the homogenous-coordinate matrix transforms of the object myself.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySetter : MonoBehaviour
{
    public GameObject parent;

    public float sensitivity = 100f;

    private Vector3 initial_position;
    private Vector3 initial_rotation;

    void Start()
    {

        initial_position = transform.position;
        initial_rotation = transform.rotation.eulerAngles;

    }

    void FixedUpdate()
    {
        //compute target rotation and destination
        Vector3 destination = parent.transform.position;
        Vector3 rot_destination = parent.transform.rotation.eulerAngles;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.transform.rotation = parent.transform.rotation; //yeah idk why I added this, but I'll just keep it to prevent more bugs lol
        
        //set rigidbody velocity (needed to have physically-accurate seeming collisions)
        Vector3 velocity = (destination - rb.transform.position) * sensitivity;
        Vector3 angular_velocity = (rot_destination - rb.transform.rotation.eulerAngles) * sensitivity;
        rb.velocity = velocity;
        rb.angularVelocity = angular_velocity;

    }
}
