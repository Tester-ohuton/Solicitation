using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textArea;

    public GameObject itemPrefab; // ゲージが満タンになった時に生成されるアイテムのPrefab
    public Transform spawnPoint; // アイテムの生成位置

    public void GenerateItem()
    {
        GameObject itemObj = Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
        itemObj.SetActive(true);
    }
}
