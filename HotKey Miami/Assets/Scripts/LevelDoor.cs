using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    [SerializeField] public List<Sequence> enemies = new();
    [SerializeField] GameObject levelCompletePanel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                }
            }
            if (enemies.Count == 0)
            {
                levelCompletePanel.SetActive(true);
            }
        }
    }
}