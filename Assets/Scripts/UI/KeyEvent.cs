using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 複数種類のキーイベント登録に対応した例
public class KeyEvent : MonoBehaviour
{
    [System.Serializable]
    public class KeyUnityEvent : UnityEvent { }

    // Inspectorから設定できるようにする
    [Tooltip("反応させたいキーを指定")]
    public KeyCode targetKey = KeyCode.Space;

    [Tooltip("キーが押された時に呼び出すイベント")]
    public KeyUnityEvent onKeyDown;
    [Tooltip("キーが離された時に呼び出すイベント")]
    public KeyUnityEvent onKeyUp;
    [Tooltip("キーが押されている間呼び出すイベント")]
    public KeyUnityEvent onKeyHeld;

    // 機能例: Inspectorから自由にUnityEventへ関数を追加可能
    void Update()
    {
        if (Input.GetKeyDown(targetKey))
            onKeyDown.Invoke();

        if (Input.GetKeyUp(targetKey))
            onKeyUp.Invoke();

        if (Input.GetKey(targetKey))
            onKeyHeld.Invoke();
    }
}

