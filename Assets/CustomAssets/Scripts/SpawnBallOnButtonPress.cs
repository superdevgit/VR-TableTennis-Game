/*
 * This script makes it possible for the player to spawn in balls at the position of his/her left controller.
 * We need to add a couple of components to the balls so the mirror-motion gamemode works.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBallOnButtonPress : MonoBehaviour
{
    public bool enabled = true;
    public bool enableMirrorMotion = true;
    public GameObject ball;
    public GameObject controller;
    //public Text point_canvas;
    public GameObject targetSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {

            if (OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four))
            {
                if (enableMirrorMotion)
                {
                    //spawn two balls, one on each side
                    GameObject ball1 = Instantiate(ball, controller.transform.position, controller.transform.rotation);
                    GameObject ball2 = Instantiate(ball, controller.transform.position, controller.transform.rotation);

                    //add the mirror-object script and set its necessary attributes
                    ball1.AddComponent<MirrorMotion>();
                    ball2.AddComponent<MirrorMotion>();

                    ball1.GetComponent<MirrorMotion>().otherObject = ball2;
                    ball2.GetComponent<MirrorMotion>().otherObject = ball1;

                    ball1.GetComponent<MirrorMotion>().dominant = false;
                    ball2.GetComponent<MirrorMotion>().dominant = true;
                }
                else
                {
                    //if we're not in the mirror-motion gamemode
                    GameObject ball1 = Instantiate(ball, controller.transform.position, controller.transform.rotation);
                    ball1.GetComponent<PointResetterScript>().targetSpawner = targetSpawner;
                }
            }
            
        }
    }
}
