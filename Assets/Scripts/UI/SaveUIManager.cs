using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveUIManager : MonoBehaviour
{
    public int stage_num; // スコア変数
    public Button iti;
    public Button ni;
    public Button san;

    public TextMeshProUGUI[] timeText;

    private string saveFilePath;

    // Use this for initialization
    void Start()
    {
        // Define the save file path
        saveFilePath = Application.persistentDataPath + "/savefile.json";

        // Check if save data exists and update the continue button
        CheckForSaveData();

        //現在のstage_numを呼び出す
        stage_num = PlayerPrefs.GetInt("BUTTON", 0);

        string thisSceneName = SceneManager.GetActiveScene().name;

        ni.interactable = false;
        san.interactable = false;

        iti.onClick.AddListener(() =>
        {
            ni.interactable = true;
            ButtonPress(0);
        });

        ni.onClick.AddListener(() =>
        {
            san.interactable = true;
            ButtonPress(1);
        });

        san.onClick.AddListener(() =>
        {
            ButtonPress(2);
        });
    }

    void ButtonPress(int i)
    {
        timeText[i].text = GameManager.instance.GetFormattedGameTime();
    }

    void CheckForSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            // Enable the continue button if save data exists
            iti.interactable = true;
        }
        else
        {
            // Disable the continue button if no save data exists
            iti.interactable = false;
        }
    }
}
