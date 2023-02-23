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
            rb.velocity += new Vector3(0, 0, speed);
            transform.forward = rb.velocity;
        }
        if (Input.GetKey(backward))
        {
            rb.velocity += new Vector3(0, 0, -speed);
            transform.forward = rb.velocity;
        }
        if (Input.GetKey(left))
        {
            rb.velocity += new Vector3(-speed, 0, 0);
            transform.forward = rb.velocity;
        }
        if (Input.GetKey(right))
        {
            rb.velocity += new Vector3(speed, 0, 0);
            transform.forward = rb.velocity;
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
            rb.velocity *= 0.95f;
            if (rb.velocity.magnitude < 0.5f)
            {
                rb.velocity = Vector3.zero;
            }
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

