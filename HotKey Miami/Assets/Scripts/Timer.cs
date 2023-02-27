using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public float levelTimer;
    [HideInInspector] public bool stopTime;
    float reset;
    [SerializeField] TextMeshProUGUI timerUI;
    [SerializeField] GameObject failPanel;

    // Start is called before the first frame update
    void Start()
    {
        reset = levelTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopTime)
        {
            levelTimer -= Time.deltaTime;
        }
        if (timerUI != null)
        {
            timerUI.text = $"Time: {(int)levelTimer}";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            //levelTimer = reset; //commented out for now since enemies will not reset.
        }
    }

}
