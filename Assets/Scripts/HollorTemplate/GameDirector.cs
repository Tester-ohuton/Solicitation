using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [TextArea(1, 10)]
    public string textArea;

    public AnomalyManager anomalyManager;

    // �C���X�y�N�^�[�Ŏw�肷�邽�߂̕ϐ�
    public int startRange1;
    public int endRange1;
    public int startRange2;
    public int endRange2;
    public int startRange3;
    public int endRange3;
    public int startRange4;
    public int endRange4;

    void Start()
    {
    }

    public void StartGame()
    {
        anomalyManager.ResetAnomalies();
        // �Q�[���J�n���̏����ݒ�

        for (int i = startRange1; i < endRange1; i++) // �C���X�y�N�^�[�Ŏw�肵���͈�
        {
            anomalyManager.TriggerAnomaly(i);
        }
    }

    public void Date2Game()
    {
        for (int i = startRange2; i < endRange2; i++) // �C���X�y�N�^�[�Ŏw�肵���͈�
        {
            anomalyManager.TriggerAnomaly(i);
        }
    }

    public void Date3Game()
    {
        for (int i = startRange3; i < endRange3; i++) // �C���X�y�N�^�[�Ŏw�肵���͈�
        {
            anomalyManager.TriggerAnomaly(i);
        }
    }

    public void Date4Game()
    {
        for (int i = startRange4; i < endRange4; i++) // �C���X�y�N�^�[�Ŏw�肵���͈�
        {
            anomalyManager.TriggerAnomaly(i);
        }
    }

    public void Retry()
    {
        StartGame();
        // ���g���C���̏���

        // PlayerPrefs�ɕۑ����ꂽ���ׂẴf�[�^���폜
        PlayerPrefs.DeleteAll();
    }
}
