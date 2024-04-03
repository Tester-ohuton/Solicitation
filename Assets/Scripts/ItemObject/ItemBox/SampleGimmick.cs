using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleGimmick : MonoBehaviour
{
    // やりたいこと
    // アイテムCubeを持っている状態で、クリックすると消える
    // ・クリック判定
    // ・アイテム持ってますよ判定

    //ギミックを解除するアイテムを外で設定できるようにする
    [SerializeField] Item.Type clearItem = default;

    public void OnClickObj()
    {
        Debug.Log("クリックしたよ！");

        // アイテムCubeを持っているかどうか
        bool Clear = ItemBox.instance.TryUseItem(clearItem);
        if (Clear == true) // クリアアイテムを持っている場合
        {
            Debug.Log("ギミック解除");
            gameObject.SetActive(false);
        }
    }
}