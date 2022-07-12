/* 
 * This script controls the ball collision sounds. It will play one of several sounds randomly on a collision.
 * If it collides with anything but the bat, it will play a "table collision" sound.
 * If it collides with the bat, it will play "bat collision" sound.
 * 
 * The volume of the sound is controlled by the relative velocity of the rigidbodies of the colliding objects (so the sound is louder if you hit it harder, up to a maximum).
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollisionSound : MonoBehaviour
{
    public AudioClip[] bounce;
    public AudioClip[] bat;
    public float maxVelocity = 1f;
    public float volume = 1f;
    public float playbackThreshold = 0.1f;
    private float lastSound = 0f;
    public List<GameObject> colliderList = new List<GameObject>();
    public int len = 0;

    private void OnCollisionEnter(Collision col)
    {
        if (!(colliderList.Contains(col.gameObject))){
            colliderList.Add(col.gameObject);
            Rigidbody rb = GetComponent<Rigidbody>();
            Rigidbody otherRB = col.gameObject.GetComponent<Rigidbody>();

            float velocity = 0f;
            //compute relative velocity, if possible
            if (otherRB)
            {
                velocity = rb.velocity.magnitude + otherRB.velocity.magnitude;
            }
            else
            {
                velocity = rb.velocity.magnitude;
            }

            volume = GetComponent<AudioSource>().volume;


            //if (lastSound - Time.time > playbackThreshold)
            //{
            // lastSound = Time.time;

            //if it's the bat, play bat collision sounds
            if (col.gameObject.name.Contains("Tischtennis"))
            {
                int clip_id = Random.Range(0, bat.Length);

                GetComponent<AudioSource>().clip = bat[clip_id];
                //volume depends on relative velocity
                GetComponent<AudioSource>().volume = Mathf.Min(1f, velocity / maxVelocity * volume);
                GetComponent<AudioSource>().Play();

            }
            //if it's not the bat, play table collision sounds
            else
            {
                int clip_id = Random.Range(0, bounce.Length);

                GetComponent<AudioSource>().clip = bounce[clip_id];
                //volume depends on relative velocity
                GetComponent<AudioSource>().volume = Mathf.Min(1f, velocity / maxVelocity * volume);
                GetComponent<AudioSource>().Play();
            }
        }
        len = colliderList.Count;
        
    }
    private void OnCollisionExit(Collision collision)
    {
        //debugging purposes
        colliderList.Remove(collision.gameObject);
        len = colliderList.Count;
    }

}
