/* 
 * This script controls the interactability and logic of the "mode switcher" button at the side of the court.
 * If you push down on the button, it will try to fall back to it's original position. To that end, a force gets applied whenever
 * the button is not in its original position.
 * When the button is pressed below a certain height, the gamemode will switch.
 * Concretely, the script will go over all objects in the scene which are relvant for the gamemode switch and either turn them on or off
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMotionAndAction : MonoBehaviour
{

    Vector3 initialPosition;

    bool buttonEnabled = false;
    bool buttonPressed = false;

    Color disabledColor = Color.red;
    Color enabledColor = Color.green;

    public GameObject playerClone; 
    public GameObject ballSpawner;
    public GameObject targetSpawner;

    public Text point_canvas;
    public Text high_score_canvas;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
            //rigidbody used to apply "spring force" of button
            Rigidbody rb = GetComponent<Rigidbody>();
            //compute and apply force/velocity towards initial position
            Vector3 velocity = (initialPosition - rb.transform.position) * 20f;
            rb.velocity = velocity;
            
            //button is below activation threshold
            if (transform.position.y <= 1.015f && !buttonPressed)
            {
                buttonPressed = true;
                buttonEnabled = !buttonEnabled;
                
                //button enabled -> button is red
                if (buttonEnabled)
                {
                    
                    GetComponent<MeshRenderer>().material.color = enabledColor;

                    //set gamemode to the "mirroring" gamemode
                    playerClone.GetComponent<MirrorMotion>().enabled = true;
                    ballSpawner.GetComponent<SpawnBallOnButtonPress>().enableMirrorMotion = true;
                    targetSpawner.GetComponent<SpawnTargets>().enabled = false;
                    targetSpawner.GetComponent<SpawnTargets>().DestroyCurrentObject();

                    point_canvas.enabled = false;
                    high_score_canvas.enabled = false;

                }
                //button disabled -> button is green
                else
                {
                    GetComponent<MeshRenderer>().material.color = disabledColor;

                    //set gamemode to the "target hitting" gamemode
                    playerClone.GetComponent<MirrorMotion>().enabled = false;
                    ballSpawner.GetComponent<SpawnBallOnButtonPress>().enableMirrorMotion = false;
                    targetSpawner.GetComponent<SpawnTargets>().enabled = true;
                    targetSpawner.GetComponent<SpawnTargets>().resetCounter();
                    targetSpawner.GetComponent<SpawnTargets>().SpawnNewTarget();

                    point_canvas.enabled = true;
                    point_canvas.text = "Points: 0";
                    high_score_canvas.enabled = true;
                }
            GetComponent<AudioSource>().Play();
            }
        //release button (necessary so the gamemode doesn't just "flipflop" back and forth)
        if (transform.position.y > 1.02f)
            {
                buttonPressed = false;
            }

        //prevent the button from being "pushed away"
        if (transform.position.y <= 1.01f)
        {
            transform.position= new Vector3(transform.position.x, 1.01f, transform.position.z);
        }
        if (transform.position.y >= 1.024f)
        {
            transform.position = new Vector3(transform.position.x, 1.024f, transform.position.z);
        }
    }

}
