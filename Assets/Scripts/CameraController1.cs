using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{
    public Camera mainCamera;
    public Camera topCamera;
    public Camera backCamera;

    public int CameraState;
    
    void Start()
    {
        //mainCamera = Camera.main;
        CameraState = 1;
        mainCamera.enabled = true;      
        topCamera.enabled = false;
        backCamera.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CameraState == 1)
            {
                CameraState = 2;
                mainCamera.enabled = false;
                topCamera.enabled = true;
            }

            else if (CameraState == 2)
            {
                CameraState = 3;
                topCamera.enabled = false;
                backCamera.enabled = true;
            }

            else if (CameraState == 3) {
                CameraState = 1;
                backCamera.enabled = false;
                mainCamera.enabled = true;

            }
        }

    }
}
