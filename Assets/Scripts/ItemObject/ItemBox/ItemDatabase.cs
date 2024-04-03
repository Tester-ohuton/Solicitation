using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] ItemListEntity ItemListEntity = default;

    //Itemをtypeから生成する
    public Item spawn(Item.Type type)
    {
        for (int i = 0; i < ItemListEntity.itemList.Count; i++)
        {
            Item itemData = ItemListEntity.itemList[i];

            //データベースの中からtypeが一致するものを探す
            if (itemData.type == type)
            {
                //一致したら、Itemを生成して渡す
                return new Item(itemData.type,itemData.sprite);
            }
        }
        return null;
    }
}