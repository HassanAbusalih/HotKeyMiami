using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;
    [SerializeField] float mouseSensitivity = 100f;
    float mouseX = 0f;
    float mouseY = 0f;

    // Update is called once per frame
    void Update()
    {
        player.transform.forward = transform.forward;
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        if (mouseX < 0)
        {
            mouseX += 360;
        }
        if (mouseX > 360)
        {
            mouseX -= 360;
        }
        if (mouseY < -90)
        {
            mouseY = -90;
        }
        if (mouseY > 90)
        {
            mouseY = 90;
        }
        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
        player.transform.rotation = transform.rotation;
        transform.position = player.transform.position + offset;
    }
}
