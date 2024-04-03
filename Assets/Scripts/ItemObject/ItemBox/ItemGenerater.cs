using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerater : MonoBehaviour
{
    [SerializeField] ItemListEntity itemListEntity = default;

    // どこでも実行できるやつ
    public static ItemGenerater instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // PickupObjスクリプトのstart関数で実行する
    public Item Spawn(Item.Type type)
    {
        // itemListの中からtypeと一致したら同じitem(データ)を生成して渡す
        foreach (Item item in itemListEntity.itemList)
        {
            if (item.type == type)
            {
                return new Item(item.type, item.sprite);
            }
        }
        return null;
    }
}