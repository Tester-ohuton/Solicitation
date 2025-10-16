using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemLogger
{
    static List<string> parts = new List<string>();
    //リスト内の数をintで召喚出来る
    //if文等で使える
    public static int Count => parts.Count;

    public static void Clear()
    {
        parts.Clear();
        //アイテム数をクリア
    }

    public static void Add(string GetitemID)
    {
        if (Contains(GetitemID))
        {
            //取得状態は追加するタイミングでもチェック!
            return;
        }
        parts.Add(GetitemID);
        //ゲットしたアイテムを記録
    }

    //アイテムをゲットしたか確認
    public static bool Contains(string CheckItemID)
    {
        return parts.Contains(CheckItemID);
    }
}