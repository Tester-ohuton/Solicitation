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

    public GameObject savePanel; // SavePanel�I�u�W�F�N�g

    public void Show()
    {
        ShowSavePanel();

        if (savePanel.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // SavePanel��\�����郁�\�b�h
    void ShowSavePanel()
    {
        savePanel.SetActive(true);
    }

    // SavePanel���\���ɂ��郁�\�b�h
    public void HideSavePanel()
    {
        savePanel.SetActive(false);
    }
}