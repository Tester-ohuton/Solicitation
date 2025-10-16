using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRaycaster : MonoBehaviour
{
    [Header("Rayの長さ")]
    public float rayLength = 3f;

    [Header("Player")]
    [SerializeField] GameObject playerObject;

    [Header("ItemLists")]
    [SerializeField] GameObject itemListsObject;

    private GameObject transformObject;

    private void Update()
    {
        Intaract(transformObject);
        NonIntaract(transformObject);
    }

    public void Intaract(GameObject obj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    // アイテムをプレイヤーの子オブジェクトに移動させる
                    obj = hit.collider.gameObject;
                    // アイテムをインタラクトする処理
                    if (obj == null)
                    {
                        obj.transform.SetParent(playerObject.transform);
                        obj.transform.position = new Vector3(0.5f, -0.5f, 1f); // 適切な位置に調整
                    }
                }
            }
        }
    }

    public void NonIntaract(GameObject obj)
    {
        // アイテムを置く処理
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (obj != null)
            {
                // アイテムをプレイヤーの子オブジェクトから解除
                obj.transform.SetParent(itemListsObject.transform);
                obj.transform.position = itemListsObject.transform.position; // 適切な位置に調整
                obj = null;
            }
        }
    }

}