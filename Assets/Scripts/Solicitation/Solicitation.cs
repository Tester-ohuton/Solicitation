using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solicitation : MonoBehaviour
{
    // FlagManagerへの参照
    private FlagManager flagManager;

    private void Start()
    {
        // FlagManagerのインスタンスを取得
        flagManager = FlagManager.Instance;

        if (flagManager == null)
        {
            Debug.LogError("FlagManager instance is null. Make sure FlagManager is initialized before using Solicitation.");
        }
    }

    // 全てのイベント状態をリセットするメソッド
    public void ResetAllEventFlags()
    {
        if (flagManager != null)
        {
            flagManager.ResetAllFlags();
        }
    }

    // イベントの状態を設定するメソッド
    public void SetEventStatus(string eventName, FlagManager.EventStatus status)
    {
        if (flagManager != null)
        {
            flagManager.SetEventStatus(eventName, status);
        }
    }

    // イベントの状態を取得するメソッド
    public FlagManager.EventStatus GetEventStatus(string eventName)
    {
        if (flagManager != null)
        {
            return flagManager.GetEventStatus(eventName);
        }

        return default;
    }
}