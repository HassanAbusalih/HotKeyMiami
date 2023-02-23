using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyList : MonoBehaviour
{
    public KeyPlusSprite[] allKeys;
}

[System.Serializable]
public class KeyPlusSprite
{
    public Sprite sprite;
    public KeyCode key;
}
