using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] GameObject[] positions; // List of positions to cycle through.
    [SerializeField] float moveSpeed = 1f;
    bool battle;
    bool forward = true;
    [SerializeField] int currentPos = 1;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Sequence>() != null)
        {
            battle = GetComponent<Sequence>().battle;
        }
        if (battle)
        {
            Destroy(this);
        }
        else if (!battle && positions != null)
        {
            LoopThroughPos();
        }
    }

    void LoopThroughPos()
    {
        Vector3 target = positions[currentPos].transform.position - transform.position;
        transform.position += target.normalized * moveSpeed * Time.deltaTime;
        float distanceToTarget = target.magnitude;
        if (distanceToTarget < 0.1f)
        {
            if (forward)
            {
                currentPos++;
            }
            else
            {
                currentPos--;
            }
            if (currentPos >= positions.Length)
            {
                forward = false;
                currentPos--;
            }
            else if (currentPos < 0)
            {
                forward = true;
                currentPos = 0;
            }
        }
    }
}
