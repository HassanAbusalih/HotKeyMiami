using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject thePlayer;
    Sequence[] enemies; 
    private void OnTriggerEnter(Collider other)
    {
        enemies = FindObjectsOfType<Sequence>();
        if (enemies.Length < 3 && FindObjectOfType<Timer>().levelTimer > 15)
        {
            thePlayer.transform.position = teleportTarget.transform.position;
        }
        
    }


}
