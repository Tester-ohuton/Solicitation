using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    public GameObject conversationUI; // 会話UIのPanelなど
    private bool isTalking = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTalking)
        {
            StartConversation();
        }
    }

    void StartConversation()
    {
        isTalking = true;
        Time.timeScale = 0f; // 時間停止
        conversationUI.SetActive(true); // 会話UI表示
    }

    public void EndConversation()
    {
        isTalking = false;
        Time.timeScale = 1f; // 時間再開
        conversationUI.SetActive(false); // 会話UI非表示
        Destroy(gameObject); // NPC削除（必要なら）
    }
}