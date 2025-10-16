using UnityEngine;
using UnityEngine.Events;

public class PlayerLife : MonoBehaviour
{
    public int playerLife;
    public bool isGameOver;
    public BGMSoundData.BGM warning;

    public static UnityEvent OnGameOver = new UnityEvent();

    private void Awake()
    {
        OnGameOver.RemoveAllListeners();
        OnGameOver.AddListener(() =>
        {
            isGameOver = true;
            Debug.Log($"isGameOver: {isGameOver}");
            SoundManager.Instance.PlayBGM(warning);
        });
    }

    public void Damage(int damage)
    {
        if (playerLife <= 0) return;

        playerLife -= damage;

        if (playerLife <= 0)
        {
            OnDie();
        }
    }

    void OnDie()
    {
        OnGameOver.Invoke();
    }
}
