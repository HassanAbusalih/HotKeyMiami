using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public KeyPlusSprite[] allKeys; //List of all assignable keys/inputs. Set in inspector.
    [SerializeField] List<KeyPlusSprite> enemyKeys = new(); //List of keys/inputs for battle.
    [SerializeField] TextMeshProUGUI misinputText;
    [SerializeField] Image keySprite; //Sprite to show the player which key to press.
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject levelCompletePanel;
    [HideInInspector] public bool lava;
    Vector3 startPos = new();
    PlayerControls playerControls;
    Sequence enemy;
    Timer timeRemaining;
    Rigidbody rb;
    bool battle;
    
    public AudioSource winAudio;
    public AudioSource loseAudio;
    void Start()
    {
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rb = GetComponent<Rigidbody>();
        playerControls = GetComponent<PlayerControls>();
        timeRemaining = GetComponent<Timer>();
    }

    void Update()
    {
        if (!failPanel.activeSelf && !levelCompletePanel.activeSelf) // Disables player control if either panel is activated.
        {
            // If in battle, take inputs. Otherwise, move/run instead.
            if (battle)
            {
                (bool battle, int time) battleResult = enemy.TakeInput(enemyKeys, keySprite, misinputText);
                battle = battleResult.battle;
                timeRemaining.levelTimer += battleResult.time;
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
            else
            {
                playerControls.PlayerActions(rb);
            }
        }
        if (timeRemaining.levelTimer < 0)
        {
            loseAudio.Play();
            timeRemaining.stopTime = true;
            rb.velocity = Vector3.zero;
            failPanel.SetActive(true);
        }
        if (levelCompletePanel.activeSelf)
        {
            winAudio.Play();
            timeRemaining.stopTime = true;
            rb.velocity = Vector3.zero;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            transform.position = startPos;
            lava = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if enemy, generate and store keys
        enemy = other.gameObject.GetComponent<Sequence>();
        if (enemy != null)
        {
            enemyKeys = enemy.SetKeys(allKeys);
            battle = true;
        }
    }
}
