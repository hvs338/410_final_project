using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{   
    // Start is called before the first frame update
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void Update(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void ProcessLook(Vector2 input){


        float mousex = input.x;
        float mousey = input.y;

        // calculate camer rotation

        xRotation -= (mousey * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f,80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);

        transform.Rotate(Vector3.up*(mousex*Time.deltaTime)*xSensitivity);

    }
}
