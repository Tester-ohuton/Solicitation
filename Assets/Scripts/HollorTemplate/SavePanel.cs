using UnityEngine;

public class SavePanel : MonoBehaviour
{
    public static SavePanel instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [TextArea(1,10)]
    public string textArea;

    public GameObject savePanel; // SavePanelオブジェクト

    public void Show()
    {
        ShowSavePanel();

        if (savePanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // SavePanelを表示するメソッド
    void ShowSavePanel()
    {
        savePanel.SetActive(true);
    }

    // SavePanelを非表示にするメソッド
    public void HideSavePanel()
    {
        savePanel.SetActive(false);
    }
}