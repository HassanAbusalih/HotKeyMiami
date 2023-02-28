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
    bool canJump;
    [SerializeField] float speed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float jumpHeight;
    

    public void PlayerActions(Rigidbody rb)
    {
        if (Physics.Raycast(transform.position, -transform.up, 1.6f))
        {
           
            canJump = true;
        }
        Vector3 movement = new Vector3();
        if (Input.GetKey(forward))
        {
            movement += Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        }
        if (Input.GetKey(backward))
        {
            movement -= Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        }
        if (Input.GetKey(left))
        {
            movement -= Vector3.ProjectOnPlane(transform.right, Vector3.up);
        }
        if (Input.GetKey(right))
        {
            movement += Vector3.ProjectOnPlane(transform.right, Vector3.up);
        }
        if (Input.GetKey(sprint))
        {
            movement = movement.normalized * sprintSpeed;
        }
        else
        {
            movement = movement.normalized * speed;
        }
        transform.position += new Vector3(movement.x, 0, movement.z) * Time.deltaTime;
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        if (Input.GetKeyDown(jump) && canJump)
        {
            GetComponent<AudioSource>().Play();
            rb.velocity +=  Vector3.up * jumpHeight;
            canJump = false;
        }
    }
}

[System.Serializable]
public class KeyPlusSprite
{
    public Sprite sprite;
    public KeyCode key;
}

