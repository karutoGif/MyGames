using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class scr_movement : MonoBehaviour
{
    public float speed = 1f;
    public CharacterController controller;
    public float gravity = -9.81f;
    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask groundmask;
    public float jumpheight = 4;
    public GameObject Gun;
    bool isRunning = false;
    public float runSpeed = 8;
    public float normalSpeed = 4;

    Vector3 velocity;
    bool isGrounded;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundmask);
        if(isGrounded && velocity.y < 0){

            velocity.y = -2f;

        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //Move forward
        controller.Move(move * speed * Time.deltaTime);

        //Normal jump input
        if (Input.GetButtonDown("Jump") && (isGrounded) && !isRunning) {

            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);

        }
        if (Input.GetButtonDown("Jump") && (isGrounded) && isRunning)
        {

            velocity.y = Mathf.Sqrt(jumpheight+10 * -2f * gravity);

        }

            //Run input
            if (Input.GetKey(KeyCode.CapsLock) && !Input.GetKey(KeyCode.S))
        {

            isRunning = true;
            speed = runSpeed;

        }
        if(!Input.GetKey(KeyCode.CapsLock)) 
        {
            isRunning = false;
            speed = normalSpeed;

        }

        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }
}
