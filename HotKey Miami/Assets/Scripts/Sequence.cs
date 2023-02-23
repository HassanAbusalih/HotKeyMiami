using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Sequence : MonoBehaviour
{
    [SerializeField] KeyList keyList; //List of all assignable keys/inputs. Set in inspector.
    [SerializeField] int keyNumber; //Number of keys/inputs the enemy will have. Set in inspector.
    int count;
    int misinput = 0;
    [SerializeField] TextMeshProUGUI misinputText;
    [SerializeField] List<KeyPlusSprite> enemyKeys = new(); //List of keys/inputs this enemy will require.
    [SerializeField] Image keySprite; //Sprite to show the player which key to press.
    bool start = true;
    // Start is called before the first frame update
    void Start()
    {
        SetKeys(keyNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            start = true;
            SetKeys(keyNumber);
        }
        TakeInput();
    }

    void TakeInput() //Scrolls through the list of keys set for the enemy as the player makes the inputs.
    {
        if (enemyKeys.Count >= 1)
        {
            keySprite.sprite = enemyKeys[count].sprite;
            if (misinput == 3)
            {
                enemyKeys.Clear();
                misinput = 0;
            }
            if (Input.GetKeyDown(enemyKeys[count].key) && misinput < 3)
            {
                enemyKeys.RemoveAt(count);
                count--;
            }
            else if (Input.anyKeyDown)
            {
                misinput++;
            }
            misinputText.text = $"Misinput: {misinput}";
        }
        if (count < 0)
        {
            keySprite.sprite = null;
            misinput = 0;
        }
    }

    void SetKeys(int numberOfKeys) //Assigns the keys the player will have to press to beat this enemy.
    {
        count = keyNumber - 1;
        for (int i = numberOfKeys; i > 0; i--)
        {
            if (start)
            {
                int randomNumber = Random.Range(0, keyList.allKeys.Length);
                KeyPlusSprite newKey = keyList.allKeys[randomNumber];
                enemyKeys.Add(newKey);
                start = false;
            }
            else
            {
                int randomNumber = Random.Range(0, keyList.allKeys.Length - 1);
                KeyPlusSprite newKey = keyList.allKeys[randomNumber];
                if (newKey == enemyKeys[enemyKeys.Count - 1])
                {
                    newKey = keyList.allKeys[randomNumber + 1];
                }
                enemyKeys.Add(newKey);
            }
        }
    }
}
