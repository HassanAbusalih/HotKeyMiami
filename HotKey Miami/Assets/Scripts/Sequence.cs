using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Sequence : MonoBehaviour
{
    [SerializeField] int keyNumber; //Number of keys/inputs the enemy will have. Set in inspector.
    public bool battle = false;
    int misinput = 0;

    public bool TakeInput(List<KeyPlusSprite> enemyKeys, Image keySprite, TextMeshProUGUI misinputText) //Scrolls through the list of keys set for the enemy as the player makes the inputs.
    {
        if (enemyKeys.Count >= 1)
        {
            if (!keySprite.gameObject.activeSelf)
            {
                battle = true;
                keySprite.gameObject.SetActive(true);
                misinputText.gameObject.SetActive(true);
            }
            keySprite.sprite = enemyKeys[enemyKeys.Count - 1].sprite;
            if (misinput == 3)
            {
                //battle lost - not currently implemented
                ResolveBattle(keySprite, misinputText);
                return false;
            }
            if (Input.GetKeyDown(enemyKeys[enemyKeys.Count - 1].key) && misinput < 3)
            {
                enemyKeys.RemoveAt(enemyKeys.Count - 1);
            }
            else if (Input.anyKeyDown)
            {
                misinput++;
            }
        }
        misinputText.text = $"Misinput: {misinput}";
        if (enemyKeys.Count < 1)
        {
            //battle won
            ResolveBattle(keySprite, misinputText);
            return false;
        }
        else
        {
            return true;
        }
    }

    void ResolveBattle(Image keySprite, TextMeshProUGUI misinputText)
    {
        keySprite.sprite = null;
        misinput = 0;
        keySprite.gameObject.SetActive(false);
        misinputText.gameObject.SetActive(false);
        battle = false;
    }

    public List<KeyPlusSprite> SetKeys(KeyPlusSprite[] keyList) //Assigns the keys the player will have to press to beat this enemy.
    {
        List<KeyPlusSprite> enemyKeys = new();
        bool start = true;
        for (int i = keyNumber; i > 0; i--)
        {
            if (start)
            {
                int randomNumber = Random.Range(0, keyList.Length);
                KeyPlusSprite newKey = keyList[randomNumber];
                enemyKeys.Add(newKey);
                start = false;
            }
            else
            {
                int randomNumber = Random.Range(0, keyList.Length - 1);
                KeyPlusSprite newKey = keyList[randomNumber];
                if (newKey == enemyKeys[enemyKeys.Count - 1])
                {
                    newKey = keyList[randomNumber + 1];
                }
                enemyKeys.Add(newKey);
            }
        }
        return enemyKeys;
    }
}
