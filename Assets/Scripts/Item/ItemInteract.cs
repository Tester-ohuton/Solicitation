using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 ItemInteract.cs
アイテム管理クラス
 */
public class ItemInteract : MonoBehaviour
{
    public static UnityEvent onInteract = new UnityEvent();
    public static UnityEvent onNonInteract = new UnityEvent();

    [Header("プレイヤーに追従する")]
    public GameObject player;
    
    [Header("プレイヤーの子オブジェクト")]
    public GameObject itemPos;

    [Header("Item")]
    public GameObject itemObject;

    [Header("ItemRoot")]
    public Transform itemRoot;

    private void Start()
    {
        Position();
        PlayerPosition();

        Instantiate(itemObject,Position(),Quaternion.identity, itemRoot);
    }

    private void Update()
    {
        Position();
        PlayerPosition();
    }

    public Vector3 Position() // 元の位置
    {
        return transform.position;
    }

    public Vector3 PlayerPosition() // インタラクトの位置
    {
        Vector3 position = 
            player.transform.position +
            itemPos.transform.position;
        return position;
    }
}