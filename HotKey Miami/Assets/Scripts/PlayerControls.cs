using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] KeyCode forward;
    [SerializeField] KeyCode backward;
    [SerializeField] KeyCode left;
    [SerializeField] KeyCode right;
    [SerializeField] KeyCode sprint;
    [SerializeField] KeyCode jump;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float maxSprintSpeed;



    public void PlayerActions(Rigidbody rb)
    {
        if (Input.GetKey(forward))
        {
            rb.velocity += transform.forward * speed;
        }
        if (Input.GetKey(backward))
        {
            rb.velocity -= transform.forward * speed;
        }
        if (Input.GetKey(left))
        {
            rb.velocity -= transform.right * speed;
        }
        if (Input.GetKey(right))
        {
            rb.velocity += transform.right * speed;
        }
        if (Input.GetKey(sprint))
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSprintSpeed);
        }
        else
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
        if (!Input.GetKey(forward) && !Input.GetKey(backward) && !Input.GetKey(left) && !Input.GetKey(right))
        {
            rb.velocity = Vector3.zero;
        }
        if (Input.GetKey(jump))
        {
            //jump code here, maybe
        }
    }
}

[System.Serializable]
public class KeyPlusSprite
{
    public Sprite sprite;
    public KeyCode key;
}

