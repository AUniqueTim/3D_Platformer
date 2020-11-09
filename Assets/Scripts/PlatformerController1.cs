using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController1 : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;
    public Rigidbody playerRB;

    private float gravity = -9.87f;

    public int playerSpeed;
    public int walkSpeed;
    public int runSpeed;

    public bool jumpAllowed;
    public bool isJumping;
    public bool grounded;

    public int jumpHeight;
    public int jumpSpeed;
    public int fallSpeed;
    




    public void Awake()
    {
        jumpAllowed = true;


    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = runSpeed;
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = walkSpeed;
        }
        
    }
    private void FixedUpdate()
    {

        //Player Transform Translation.
        if (Toolbox.Instance.m_CameraController.CameraState == 1)
        {
            if (Input.GetKey(KeyCode.D)) { transform.Translate(Vector3.right * playerSpeed * Time.deltaTime); }  //FORWARD
            if (Input.GetKey(KeyCode.A)) { transform.Translate(Vector3.left * playerSpeed * Time.deltaTime); }  //BACK
            if (Input.GetKey(KeyCode.W)) { transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime); }  //Z-AXIS POSITIVE
            if (Input.GetKey(KeyCode.S)) { transform.Translate(Vector3.back * playerSpeed * Time.deltaTime); }  //Z-AXIS NEGATIVE
        }
        else if (Toolbox.Instance.m_CameraController.CameraState == 2)
        {
            if (Input.GetKey(KeyCode.W)) { transform.Translate(Vector3.right * playerSpeed * Time.deltaTime); }  //FORWARD
            if (Input.GetKey(KeyCode.S)) { transform.Translate(Vector3.left * playerSpeed * Time.deltaTime); }  //BACK
            if (Input.GetKey(KeyCode.A)) { transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime); }  //Z-AXIS POSITIVE
            if (Input.GetKey(KeyCode.D)) { transform.Translate(Vector3.back * playerSpeed * Time.deltaTime); }  //Z-AXIS NEGATIVE
        }
        else if (Toolbox.Instance.m_CameraController.CameraState == 3)
        {
            if (Input.GetKey(KeyCode.W)) { transform.Translate(Vector3.right * playerSpeed * Time.deltaTime); }  //FORWARD
            if (Input.GetKey(KeyCode.S)) { transform.Translate(Vector3.left * playerSpeed * Time.deltaTime); }  //BACK
            if (Input.GetKey(KeyCode.A)) { transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime); }  //Z-AXIS POSITIVE
            if (Input.GetKey(KeyCode.D)) { transform.Translate(Vector3.back * playerSpeed * Time.deltaTime); }  //Z-AXIS NEGATIVE
        }



        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpAllowed) {  Jump(); }
               
        if (grounded == false)
            {
                CancelJump();
            }
        else if (grounded == true)
        {
            isJumping = false;
        }

    }


    public void Jump()
    {
        playerRB.transform.position += Vector3.up * jumpHeight * jumpSpeed  * -gravity * Time.deltaTime;
        isJumping = true;
        jumpAllowed = false;
        
        Debug.Log("Jumped.");
            

    }
    public void CancelJump()
    {
        isJumping = false;
        Debug.Log("Is Jumping.");
        
    }
    public void OnTriggerEnter(Collider other)
    {
        grounded = true;
        jumpAllowed = true;
    }
    public void OnTriggerExit(Collider other)
    {
        grounded = false;
    }




}
