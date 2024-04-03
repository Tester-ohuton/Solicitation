using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObj : MonoBehaviour
{
    public static PickupObj instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] Item.Type itemType = default; //defaultを入れると警告が出なくなる
    Item item = default;


    private void Start()
    {
        //itemTypeに応じてitemを生成する(アイテムに対してTypeとSpriteを設定する）
        item = ItemGenerater.instance.Spawn(itemType);
    }

    //クリックしたら消す
    public void OnPointerClick()
    {
        Debug.Log("OnClickObj");
        ItemBox.instance.SetItem(item);
        gameObject.SetActive(false);
    }
}