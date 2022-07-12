/*
 * This script is the heart of the "mirroring" gamemode. If applied to an object, it will replicate the motion of the object assigned to it, but
 * axis mirrored around the y-axis.
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMotion : MonoBehaviour
{

    public bool enabled = true;
    public GameObject otherObject; //the object to be mirrored

    //determines which ball/bat should "give the motion"
    //the physics are only used for one object and the other one just mirror-copies its position
    //this is necessary because otherwise the objects wouldn't behave in a "natural" way
    public bool dominant = true; 

    // Start is called before the first frame update
    void Start()
    {
        if (enabled)
        {
            Vector3 other_position = otherObject.transform.position;
            transform.position = new Vector3(-other_position.x, other_position.y, -other_position.z);
            Physics.IgnoreCollision(GetComponent<Collider>(), otherObject.GetComponent<Collider>());
        }
        else
        {
            //place the object below the surface so the player won't see it (but we also don't have to destroy it)
            //this is used to make it possible to change the gamemode without thestroying the bat
            Vector3 position = new Vector3(0, -20, 0);
            transform.position = position;
        }
    }

    void Update()
    {
        if (enabled)
        {
            if (!dominant)
            {
                //only set the color for balls, but not for the bats
                if (!name.Contains("Tischtennis"))
                {
                    //set color to green if the ball is the submissive one
                    GetComponent<MeshRenderer>().material.color = new Color(1, 0, 1, 1);
                }

                //set mirror-position and rotation
                Vector3 other_position = otherObject.transform.position;
                transform.position = new Vector3(-other_position.x, other_position.y, -other_position.z);
                Vector3 other_rotation = otherObject.transform.rotation.eulerAngles;
                //rotation of bat needs to be handled a little differently
                if (name.Contains("Tischtennis")) { 
                    transform.rotation = Quaternion.Euler(other_rotation.x, other_rotation.y + 180f, other_rotation.z); 
                }
                else { 
                    transform.rotation = Quaternion.Euler(-other_rotation.x, other_rotation.y, -other_rotation.z); 
                }

                Rigidbody rb = GetComponent<Rigidbody>();
                Rigidbody other_rb = otherObject.GetComponent<Rigidbody>();

                Vector3 other_velocity = other_rb.velocity;

                rb.velocity = new Vector3(-other_velocity.x, other_velocity.y, -other_velocity.z);
            }
            else
            {
                if (!name.Contains("Tischtennis"))
                {
                    //set color to red if the ball is the dominant one (OwO what's this? *notices your dominance*)
                    GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0, 0.6f);
                }
            }
        }
        else
        {
            Vector3 position = new Vector3(0, -20, 0);
            transform.position = position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enabled)
        {
            //important: if a ball is hit by the player, the mode must be set to dominant on the colliding ball
            //this is because otherwise the object won't react to being hit by the bat
            if (collision.gameObject.name.Contains("Player"))
            {
                dominant = true;
                otherObject.GetComponent<MirrorMotion>().dominant = false;
                GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
                otherObject.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
            }
        }
    }
}
