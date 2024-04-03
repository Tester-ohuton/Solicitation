using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePointerpos : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D cursorTexture1;
    public Texture2D cursorTexture2;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture2, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(cursorTexture1, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(cursorTexture2, Vector2.zero, CursorMode.Auto);
    }

}
