using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float levelTimer;
    float battleTime;
    bool endOfBattle;
    [SerializeField] TextMeshProUGUI timerUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer -= Time.deltaTime;
        if (timerUI != null)
        {
            timerUI.text = $"Time: {(int)levelTimer}";
        }
        if (levelTimer < 0)
        {
            //level failed, reset
        }
    }


}
