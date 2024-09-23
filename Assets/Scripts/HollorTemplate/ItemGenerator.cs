using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textArea;

    public GameObject itemPrefab; // �Q�[�W�����^���ɂȂ������ɐ��������A�C�e����Prefab
    public Transform spawnPoint; // �A�C�e���̐����ʒu

    public void GenerateItem()
    {
        GameObject itemObj = Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
        itemObj.SetActive(true);
    }
}
