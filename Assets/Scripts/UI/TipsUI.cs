using UnityEngine;

public class TipsUI : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textArea;

    public GameObject itemPrefab; // ゲージが満タンになった時に生成されるアイテムのPrefab

    public void Destroier()
    {
        itemPrefab.SetActive(false);
    }
}
