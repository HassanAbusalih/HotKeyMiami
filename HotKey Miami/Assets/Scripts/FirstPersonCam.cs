using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;
    [SerializeField] float mouseSensitivity = 100f;
    float xRotation = 0f;
    float yRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation -= mouseY;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        player.transform.rotation = Quaternion.Euler(yRotation, xRotation, 0f);
        transform.position = player.transform.position + offset;
        transform.forward = player.transform.forward;
        //transform.rotation = player.transform.rotation;
    }
}
