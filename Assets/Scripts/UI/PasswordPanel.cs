using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordPanel : MonoBehaviour
{
    [Space]
    [SerializeField] private DoorController entranceDoorController;

    [Space]
    [SerializeField] private EntranceUIManager entranceUIManager;

    [Space]
    [SerializeField] private ClearEffect clearEffect;

    [Space]
    [SerializeField] private GameOverEffect gameOverEffect;

    // �������p�X���[�h�̏���
    private List<int> correctPassword = new List<int> { 1, 7, 1, 4 };

    // �v���C���[�̓��͂�ێ����郊�X�g
    private List<int> playerInput = new List<int>();

    public void OnPasswordButtonClick(int buttonValue)
    {
        // �v���C���[�̓��͂����X�g�ɒǉ�
        playerInput.Add(buttonValue);

        // ���͂����������ǂ������`�F�b�N
        CheckPassword();
    }

    private void CheckPassword()
    {
        // �v���C���[�̓��͂��������������I�Ɋm�F
        for (int i = 0; i < playerInput.Count; i++)
        {
            if (playerInput[i] != correctPassword[i])
            {
                GameOver();
                return;
            }
        }

        // �S�Ă̓��͂��������ꍇ�A�N���A���o�����s
        if (playerInput.Count == correctPassword.Count)
        {
            Clear();
        }
    }

    private void Clear()
    {
        Debug.Log("Correct Password! Door unlocked.");
        // �h�A���A�����b�N���鏈����ǉ�
        entranceUIManager.RockUIPanelActive(false); //rockUIPanel
        entranceUIManager.HideUI(); //entranceUIPanel

        clearEffect.PlayClearEffect();
    }

    public void GameOver() /*EntranceUIManager�ł��Ă�*/
    {
        Debug.Log("Incorrect Password! Game Over.");

        // Implement the logic for opening the door
        Debug.Log("Opening the entranceDoor...");
        // Door opening animation logic would go here
        if (entranceDoorController != null)
        {
            Debug.Log("Door ��������܂����B");
            entranceDoorController.OpenDoorAnimation();
        }
        else
        {
            Debug.LogWarning("Animator ��������܂���ł����B");
        }

        // �Q�[���I�[�o�[�̉��o��ǉ�
        gameOverEffect.PlayGameOverEffect();

        playerInput.Clear(); // ���̓��Z�b�g
    }
}
