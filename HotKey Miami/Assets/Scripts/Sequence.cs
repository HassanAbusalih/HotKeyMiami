using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sequence : MonoBehaviour
{
    [SerializeField] KeyList keyList; //List of all assignable keys/inputs. Set in inspector.
    [SerializeField] int keyNumber; //Number of keys/inputs the enemy will have. Set in inspector.
    int count;
    [SerializeField] List<KeySprite> enemyKeys = new(); //List of keys/inputs this enemy will require.
    [SerializeField] Image keySprite; //Sprite to show the player which key to press.
    // Start is called before the first frame update
    void Start()
    {
        count = keyNumber - 1;
        SetKeys(keyNumber);
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
    }

    void TakeInput() //Scrolls through the list of keys set for the enemy as the player makes the inputs.
    {
        if (enemyKeys.Count >= 1)
        {
            keySprite.sprite = enemyKeys[count].sprite;
            if (Input.GetKeyDown(enemyKeys[count].key))
            {
                enemyKeys.RemoveAt(count);
                count--;
            }
        }
        if (count < 0)
        {
            keySprite.sprite = null;
        }
    }

    void SetKeys(int numberOfKeys) //Assigns the keys the player will have to press to beat this enemy.
    {
        for (int i = numberOfKeys; i > 0; i--)
        {
            KeySprite newKey = keyList.allKeys[Random.Range(0, keyList.allKeys.Length)];
            enemyKeys.Add(newKey);
        }
    }
}
