using UnityEngine;
using UnityEngine.Events;

public class TipsUI : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textArea;

    public GameObject itemPrefab; // ゲージが満タンになった時に生成されるアイテムのPrefab

    public static UnityEvent onDestroier = new UnityEvent();

    private void Start()
    {
        onDestroier.RemoveAllListeners();
        onDestroier.AddListener(Destroier);
    }

    public void Destroier()
    {
        itemPrefab.SetActive(false);
    }
}
