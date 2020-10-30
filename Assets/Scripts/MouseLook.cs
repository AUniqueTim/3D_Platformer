using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Rigidbody playerRB;
    public static Quaternion QuaternionX;
    public static Quaternion QuaternionY;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 };
    public RotationAxes axes = RotationAxes.MouseXAndY;
    float rotationX = 0f;
    float rotationY = 0f;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;
    public float minimumX = -360f;
    public float maximumX = 360f;
    public float minimumY = -60f;
    public float maximumY = 60f;
    Quaternion originalRotation;

    public Vector3 localRotation;
    

    public static bool rotatingXY;

        public Transform cameraFollowTarget;

    Vector2 rotation = new Vector2(0, 0);
    
    public float lookSpeed = .5f;

    private void Start()
    {
        if (playerRB)
            playerRB.freezeRotation = true;
        originalRotation = transform.localRotation;
    }

    private void Update()
    {
        

        transform.LookAt(cameraFollowTarget);
        //FreeLook Camera:
        //rotation.y += Input.GetAxis("Mouse X");
        //rotation.x += -Input.GetAxis("Mouse Y");
        //transform.eulerAngles = (Vector2)rotation * lookSpeed;

        if (axes == RotationAxes.MouseXAndY)

        {
            rotatingXY = true;
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);


            playerRB.rotation = originalRotation * xQuaternion;


        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }

    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    
}
