using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Sequence : MonoBehaviour
{
    [SerializeField] int keyNumber; //Number of keys/inputs the enemy will have. Set in inspector.
    [SerializeField] int timeReward; //Amount of time to reward player on win.
    [SerializeField] int timePenalty; //Amount of time to penalize the player on loss. Should be a negative value or zero.
    [SerializeField] Image inputTimeVisual;
    [SerializeField] float timeForInput;
    [SerializeField] TextMeshProUGUI rating;
    float currentInputTime;
    public bool battle = false;
    int misinput = 0;

    public (bool,int) TakeInput(List<KeyPlusSprite> enemyKeys, Image keySprite, TextMeshProUGUI misinputText) //Scrolls through the list of keys set for the enemy as the player makes the inputs.
    {
        if (enemyKeys.Count >= 1)
        {
            keySprite.sprite = enemyKeys[enemyKeys.Count - 1].sprite;
            if (!keySprite.gameObject.activeSelf)
            {
                battle = true;
                keySprite.gameObject.SetActive(true);
                misinputText.gameObject.SetActive(true);
                inputTimeVisual.gameObject.SetActive(true);
                currentInputTime = timeForInput;
            }
            currentInputTime -= Time.deltaTime;
            inputTimeVisual.fillAmount = currentInputTime / timeForInput;
            if (inputTimeVisual.fillAmount > 0.7f)
            {
                inputTimeVisual.color = Color.green;
            }
            else if (inputTimeVisual.fillAmount > 0.4f)
            {
                inputTimeVisual.color = Color.yellow;
            }
            else
            {
                inputTimeVisual.color = Color.red;
            }
            if (misinput == 3)
            {
                //battle lost.
                ResolveBattle(keySprite, misinputText);
                return (false, timePenalty);
            }
            if (Input.GetKeyDown(enemyKeys[enemyKeys.Count - 1].key) && misinput < 3)
            {
                enemyKeys.RemoveAt(enemyKeys.Count - 1);
                rating.gameObject.SetActive(true);
                if (inputTimeVisual.fillAmount > 0.7f)
                {
                    rating.text = "Perfect!";
                    rating.color = Color.green;
                }
                else if (inputTimeVisual.fillAmount > 0.4f)
                {
                    rating.text = "Good!";
                    rating.color = Color.yellow;
                }
                else
                {
                    rating.text = "Poor...";
                    rating.color = Color.red;
                }
                Invoke(nameof(TurnOffRating), 0.5f);
                currentInputTime = timeForInput;
            }
            else if (Input.anyKeyDown)
            {
                misinput++;
            }
            else if (currentInputTime < 0)
            {
                enemyKeys.RemoveAt(enemyKeys.Count - 1);
                misinput++;
                currentInputTime = timeForInput;
            }
        }
        misinputText.text = $"Misinput: {misinput} / 3";
        if (enemyKeys.Count < 1)
        {
            //battle won.
            ResolveBattle(keySprite, misinputText);
            return (false, timeReward);
        }
        else
        {
            //battle continues.
            return (true, 0);
        }
    }

    void TurnOffRating()
    {
        rating.gameObject.SetActive(false);
        rating.text = null;
        if (!battle)
        {
            Destroy(this);
        }
    }

    void ResolveBattle(Image keySprite, TextMeshProUGUI misinputText)
    {
        keySprite.sprite = null;
        misinput = 0;
        keySprite.gameObject.SetActive(false);
        misinputText.gameObject.SetActive(false);
        inputTimeVisual.gameObject.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
        battle = false;
        Invoke(nameof(TurnOffRating), 0.5f);
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
