using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class scr_mouselook : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform player;
    float xRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouse_X = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouse_Y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xRotation -= mouse_Y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        player.Rotate(Vector3.up * mouse_X);
        
    }
}
