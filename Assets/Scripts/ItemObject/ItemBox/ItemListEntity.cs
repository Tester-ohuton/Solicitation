using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemListEntity : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}