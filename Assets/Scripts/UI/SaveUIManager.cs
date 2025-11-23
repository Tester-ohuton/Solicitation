using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveUIManager : MonoBehaviour
{
    public static int stage_num; // スコア変数
    public Button iti;
    public Button ni;
    public Button san;
    public Button yon;
    public Button goo;
    public Button roku;
    public Button continueButton;

    public Text[] timeText;

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

        iti.onClick.AddListener(() =>
        {
            ButtonPress(0);
        });

        ni.onClick.AddListener(() =>
        {
            ButtonPress(1);
        });

        san.onClick.AddListener(() =>
        {
            ButtonPress(2);
        });

        yon.onClick.AddListener(() =>
        {
            ButtonPress(3);
        });

        goo.onClick.AddListener(() =>
        {
            ButtonPress(4);
        });

        roku.onClick.AddListener(() =>
        {
            ButtonPress(5);
        });

        continueButton.onClick.AddListener(() =>
        {
            ButtonPress(6);
        });
    }

    void ButtonPress(int point)
    {
        timeText[point].text = GameManager.instance.GetFormattedGameTime() + $"セーブデータ{point}";
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
