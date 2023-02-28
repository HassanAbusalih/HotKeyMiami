using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorial;

    private void Start()
    {
        StartCoroutine(WaitThenClose());
    }

    IEnumerator WaitThenClose()
    {
        yield return new WaitForSeconds(2);
        yield return new WaitUntil(()=> Input.GetKey(KeyCode.Escape));
        tutorial.SetActive(false);
    }
}
