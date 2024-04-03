using System;
using UnityEngine;

[Serializable] // インスペクターで表示される
public class Item
{
    // 種類を持っている
    public enum Type
    {
        Card_1,
        Card_2,
        Card_3,
        Card_4,
        Card_5,
        Card_6,
    }
    public Type type;     // 種類
    public Sprite sprite; // Slotに表示する画像

    public Item(Type type, Sprite sprite)
    {
        this.type = type;
        this.sprite = sprite;
    }
}