/*
 * This script controls the point counting and target spawning logic for the target gamemode.
 * If you hit a target, a sound will play and the counter will increase.
 * If you hit the floor, the counter will be reset (called from a different class) and depending on whether you got
 * a new highscore or not, a different sound will play.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTargets : MonoBehaviour
{

    public bool enabled = true;

    //size options
    public float maxLateral = 0.8f;
    public float maxVertical = 1.5f;
    public float minVertical = 0.9f;
    public float x = -1.65f;
    public float targetSizeY = 0.03f;
    public float targetSizeZ = 0.03f;
    public bool enableRandomSize = true;
    public float randomSizeModifier = 0.2f;

    //prefab for the target
    public GameObject targetPrefab;
    public Color targetColor = Color.red;

    //motion options
    //I never actually used these. I could have added a "hard mode" for the targets which could have used this
    public bool enableMotion = false;
    public float maxVelocity = 1f;

    //for interface displaying later
    public int hitCounter = 0;
    public int high_score = 0;

    AudioClip hitSound;

    public Text pointDisplay;
    public Text high_score_text;

    private GameObject currentTarget;

    public bool isHighScore = false;

    public AudioClip[] soundEffects;

    void Start()
    {
        SpawnNewTarget();
    }

    public void DestroyCurrentObject()
    {
        Destroy(currentTarget);
    }

    public void resetCounter()
    {
        hitCounter = 0;
        pointDisplay.text = "Points: " + hitCounter.ToString();
        if (isHighScore)
        {
            //play highscore sound (if new highscore was reached before hitting the floor)
            GetComponent<AudioSource>().clip = soundEffects[0]; 
            GetComponent<AudioSource>().Play();
        }
        else
        {
            //play failure sound (if no new highscore was reached)
            GetComponent<AudioSource>().clip = soundEffects[1];
            GetComponent<AudioSource>().Play();

        }
        isHighScore = false;
        

    }
    public void AddHit()
    {
        //set the counter on the point counter display
        hitCounter++;
        pointDisplay.text = "Points: " + hitCounter.ToString();
        if (hitCounter > high_score)
        {
            isHighScore = true;
        }
        high_score = Mathf.Max(hitCounter, high_score);
        high_score_text.text = "High Score: " + high_score.ToString();
    }

    public void SpawnNewTarget()
    {
        if (enabled)
        {
            //if current target still exists, destroy it
            if (currentTarget)
            {
                Destroy(currentTarget);
                GetComponent<AudioSource>().clip = soundEffects[2]; //hitting success sound
                GetComponent<AudioSource>().Play();
            }
            float sizeModifierY = 1f;
            float sizeModifierZ = 1f;

            //set random size of target
            if (enableRandomSize)
            {
                sizeModifierY = Random.Range(1f + randomSizeModifier, 1f - randomSizeModifier);
                sizeModifierZ = Random.Range(1f + randomSizeModifier, 1f - randomSizeModifier);
            }

            //determine new spawn location and then spawn the target and set its color
            Vector3 spawnPosition = new Vector3(-x, Random.Range(minVertical + targetSizeY, maxVertical - targetSizeY), Random.Range(-maxLateral, maxLateral));
            Vector3 spawnSize = new Vector3(targetSizeY * sizeModifierY, 1, targetSizeZ * sizeModifierZ);
            Quaternion spawnRotation = Quaternion.Euler(0, 0, -90);
            currentTarget = Instantiate(targetPrefab, spawnPosition, spawnRotation, transform);
            currentTarget.transform.localScale = spawnSize;
            currentTarget.GetComponent<MeshRenderer>().material.color = targetColor;
        }
    }

    

}
