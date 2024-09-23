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

    // 正しいパスワードの順番
    private List<int> correctPassword = new List<int> { 1, 7, 1, 4 };

    // プレイヤーの入力を保持するリスト
    private List<int> playerInput = new List<int>();

    public void OnPasswordButtonClick(int buttonValue)
    {
        // プレイヤーの入力をリストに追加
        playerInput.Add(buttonValue);

        // 入力が正しいかどうかをチェック
        CheckPassword();
    }

    private void CheckPassword()
    {
        // プレイヤーの入力が正しいか部分的に確認
        for (int i = 0; i < playerInput.Count; i++)
        {
            if (playerInput[i] != correctPassword[i])
            {
                GameOver();
                return;
            }
        }

        // 全ての入力が正しい場合、クリア演出を実行
        if (playerInput.Count == correctPassword.Count)
        {
            Clear();
        }
    }

    private void Clear()
    {
        Debug.Log("Correct Password! Door unlocked.");
        // ドアをアンロックする処理を追加
        entranceUIManager.RockUIPanelActive(false); //rockUIPanel
        entranceUIManager.HideUI(); //entranceUIPanel

        clearEffect.PlayClearEffect();
    }

    public void GameOver() /*EntranceUIManagerでも呼ぶ*/
    {
        Debug.Log("Incorrect Password! Game Over.");

        // Implement the logic for opening the door
        Debug.Log("Opening the entranceDoor...");
        // Door opening animation logic would go here
        if (entranceDoorController != null)
        {
            Debug.Log("Door が見つかりました。");
            entranceDoorController.OpenDoorAnimation();
        }
        else
        {
            Debug.LogWarning("Animator が見つかりませんでした。");
        }

        // ゲームオーバーの演出を追加
        gameOverEffect.PlayGameOverEffect();

        playerInput.Clear(); // 入力リセット
    }
}
