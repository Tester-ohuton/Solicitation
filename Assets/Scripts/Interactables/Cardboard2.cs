using UnityEngine;
using UnityEngine.Events;

public class Cardboard2 : MonoBehaviour
{
    public UnityEvent onOpened;
    private bool isOpened;

    private void Start()
    {
        // ランタイムで結びつける例
        if (UIManager.instance != null)
            onOpened.AddListener(UIManager.instance.IncrementOpenedCardboard);
    }

    // プレイヤー操作等で段ボールを開けたときに呼ぶ
    public void Open()
    {
        if (isOpened) return;
        isOpened = true;
        onOpened?.Invoke();

        // 開ける処理（アニメーション、ドロップなど）
        // ...

        // UI に通知
        if (UIManager.instance != null)
            UIManager.instance.IncrementOpenedCardboard();
        else
            Debug.LogWarning("UIManager.instance が null です。初期化順を確認してください。");
    }

    private void OnMouseDown()
    {
        Open();
    }
}