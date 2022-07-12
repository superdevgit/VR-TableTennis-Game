/*
 * Instead of the button mode switcher I initially wanted to implement a user interface which could be interacted with by pointing at it.
 * I just couldn't get this to work though, so I abandonned this idea and went with the (much simpler) button implementation instead.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererSettings : MonoBehaviour
{

    //Declare a LineRenderer to store the component attached to the GameObject. 
    [SerializeField] LineRenderer rend;

    //Settings for the LineRenderer are stored as a Vector3 array of points. Set up a V3 array to //initialize in Start. 
    Vector3[] points;

    public LayerMask layerMask;

    //Start is called before the first frame update
    void Start()
    {
        //get the LineRenderer attached to the gameobject. 
        rend = gameObject.GetComponent<LineRenderer>();

        //initialize the LineRenderer
        points = new Vector3[2];

        //set the start point of the linerenderer to the position of the gameObject. 
        points[0] = Vector3.zero;

        //set the end point 20 units away from the GO on the Z axis (pointing forward)
        points[1] = new Vector3(0, 0, 5);//transform.position + new Vector3(0, 0, 5);

        //finally set the positions array on the LineRenderer to our new values
        rend.SetPositions(points);
        rend.enabled = true;
    }

    public void AlignLineRenderer(LineRenderer rend)
    {
        Ray ray;
        ray = new Ray(transform.position, transform.TransformVector(Vector3.forward));
        RaycastHit hit = new RaycastHit();
        

        if (Physics.Raycast(ray, out hit, layerMask))//, layerMask) && hit.collider.gameObject!=null && hit.collider.gameObject.layer==5)
        {
            Debug.Log("###############Colliding with: " + hit.collider.gameObject.name+"########################## on layer: " + hit.collider.gameObject.layer);
            points[1] = new Vector3(0, 0, hit.distance);//transform.forward + new Vector3(0, 0, hit.distance);
            rend.startColor = Color.red;
            rend.endColor = Color.red;
        }
        else
        {
            points[1] = new Vector3(0, 0, 5);//transform.forward + new Vector3(0, 0, 20);
            rend.startColor = Color.green;
            rend.endColor = Color.green;
        }
        print(hit.collider.gameObject+ " " + hit.collider.gameObject.layer);
        rend.SetPositions(points);
        rend.material.color = rend.startColor;
    }

    private void Update()
    {
         AlignLineRenderer(rend);
    }

    /*void Start()
    {
        img = panel.GetComponent<Image>();

        //get the LineRenderer attached to the gameobject. 
        rend = gameObject.GetComponent<LineRenderer>();

        //initialize the LineRenderer
        points = new Vector3[2];

        //set the start point of the linerenderer to the position of the gameObject. 
        points[0] = Vector3.zero;

        //set the end point 20 units away from the GO on the Z axis (pointing forward)
        points[1] = transform.position + new Vector3(0, 0, 20);

        //finally set the positions array on the LineRenderer to our new values
        rend.SetPositions(points);
        rend.enabled = true;
    }
    void Update()
    {
        AlignLineRenderer(rend);
        //if (AlignLineRenderer(rend) && Input.GetAxis("Submit") > 0)
        //{
        //   btn.onClick.Invoke();
        //}
    }

    public LayerMask layerMask;
     
    public bool AlignLineRenderer(LineRenderer rend)
    {
        bool hitBtn = false;
        Ray ray;
        ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //string[] target = new string[1];
        //target[0] = "UI_2";

        if (Physics.Raycast(ray, out hit, layerMask)) //&& hit.collider.gameObject.GetComponent<LayerMask>() == LayerMask.GetMask(target))
        {
            //Debug.Log("hit something");

            points[1] = transform.forward + new Vector3(0, 0, hit.distance);
            rend.startColor = Color.red;
            rend.endColor = Color.red;
            btn = hit.collider.gameObject.GetComponent<Button>();
            hitBtn = true;

        }
        else
        {
            // Debug.Log("missed");
            points[1] = transform.forward + new Vector3(0, 0, 20);
            rend.startColor = Color.green;
            rend.endColor = Color.green;
        }

        rend.SetPositions(points);
        rend.material.color = rend.startColor;
        return hitBtn;
    }
   public void ColorchangeOnClick()
    {
        if (btn != null)
        {
            if (btn.name == "red_btn")
            {
                img.color = Color.red;
            }
            else if (btn.name == "blue_btn")
            {
                img.color = Color.blue;
            }
            else if (btn.name == "green_btn")
            {
                img.color = Color.green;
            }
        } 
    }*/

}
