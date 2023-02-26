using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public KeyPlusSprite[] allKeys; //List of all assignable keys/inputs. Set in inspector.
    [SerializeField] List<KeyPlusSprite> enemyKeys = new(); //List of keys/inputs for battle.
    [SerializeField] TextMeshProUGUI misinputText;
    [SerializeField] Image keySprite; //Sprite to show the player which key to press.
    PlayerControls playerControls;
    Sequence enemy;
    Timer timeRemaining;
    Rigidbody rb;
    bool battle;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = GetComponent<PlayerControls>();
        timeRemaining = GetComponent<Timer>();
    }

    void Update()
    {
        // if in battle, take inputs. Otherwise, move/run instead.
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
