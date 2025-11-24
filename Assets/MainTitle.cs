using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // カーソルを表示する
        Cursor.visible = true;
        // カーソルのロックを解除する
        Cursor.lockState = CursorLockMode.None;
    }

    // ゲームプレイ中に他のスクリプトでカーソルが非表示にされたり、
    // ロックされたりする可能性がある場合は、Update()メソッドで
    // 常に表示・ロック解除状態を維持するように設定することもできます。
    void Update()
    {
        if (!Cursor.visible || Cursor.lockState != CursorLockMode.None)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}