using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour { 

    public Transform[] backgrounds;     //Array of all the back and foregrounds
    private float[] parallaxScales;     //Camera movement moves background
    public float smoothing = 1f;        //How smooth it moves, make sure above 0

    private Transform cam;              //refrence to main cameras tranform
    private Vector3 previousCamPos;     //Store last position of camera

    void Awake (){                      //for references
        cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;


        parallaxScales = new float[backgrounds.Length];  //asigning parallaxscales

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //for each background

        for (int i = 0; i <  backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // set target position of current position plus parallax

            float backgroundsTargetPosX = backgrounds[i].position.x + parallax;

            //create target position curent position with it's target x position

            Vector3 backgroundsTargetPos = new Vector3(backgroundsTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between positions using lerp

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundsTargetPos, smoothing * Time.deltaTime);
        }
        // set previousCamPos to the position of the end frame
        previousCamPos = cam.position;
    }
}
