using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public float levelTimer;
    float reset;
    [SerializeField] TextMeshProUGUI timerUI;

    // Start is called before the first frame update
    void Start()
    {
        reset = levelTimer;
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
            //level failed, reset. Should probably bring up UI element instead.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            //levelTimer = reset;
        }
    }

}
