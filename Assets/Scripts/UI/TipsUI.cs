using UnityEngine;

public class TipsUI : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textArea;

    public GameObject itemPrefab; // �Q�[�W�����^���ɂȂ������ɐ��������A�C�e����Prefab

    public void Destroier()
    {
        itemPrefab.SetActive(false);
    }
}
