using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyList : MonoBehaviour
{
    public KeySprite[] allKeys;
}

[System.Serializable]
public class KeySprite
{
    public Sprite sprite;
    public KeyCode key;
}
