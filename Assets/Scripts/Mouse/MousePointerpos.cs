using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePointerpos : MonoBehaviour
{
    private void Start()
    {
        // カーソルを常に表示する
        Cursor.visible = true;

        // カーソルのロックを解除する
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (!Cursor.visible || Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
