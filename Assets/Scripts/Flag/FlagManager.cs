// File path: Scripts/FlagManager.cs

using UnityEngine;

public class FlagManager : MonoBehaviour
{
    // Singletonインスタンス
    public static FlagManager Instance { get; private set; }

    // 各イベントの状態を表すEnum
    public enum EventStatus
    {
        NotStarted, // 未開始
        InProgress, // 進行中
        Completed   // 完了
    }

    // プロローグイベント
    public EventStatus Prologue_Start { get; private set; }

    // 1日目のイベント
    public EventStatus Day1_UnpackingStart { get; private set; }
    public EventStatus Day1_Visitor { get; private set; }
    public EventStatus Day1_MailDelivery { get; private set; }
    public EventStatus Day1_ReligionFlyerDelivery { get; private set; }
    public EventStatus Day1_UnpackingEnd { get; private set; }

    // 2日目のイベント
    public EventStatus Day2_Visitor1 { get; private set; }
    public EventStatus Day2_UnpackingStart { get; private set; }
    public EventStatus Day2_Visitor2 { get; private set; }
    public EventStatus Day2_ReadBook { get; private set; }

    // 3日目のイベント
    public EventStatus Day3_Visitor1 { get; private set; }
    public EventStatus Day3_Visitor2 { get; private set; }
    public EventStatus Day3_UnpackingEnd { get; private set; }
    public EventStatus Day3_Visitor3 { get; private set; }
    public EventStatus Day3_Visitor4 { get; private set; }
    public EventStatus Day3_FriendLeaves { get; private set; }
    public EventStatus Day3_AirConditionerBroken { get; private set; }

    // 4日目のイベント
    public EventStatus Day4_Visitor1 { get; private set; }
    public EventStatus Day4_Visitor2 { get; private set; }
    public EventStatus Day4_Visitor3 { get; private set; }
    public EventStatus Day4_ChoiceSuccess { get; private set; }
    public EventStatus Day4_ChoiceFailure { get; private set; }

    private void Awake()
    {
        // Singletonパターンの実装
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // イベントの状態を設定するメソッド
    public void SetEventStatus(string eventName, EventStatus status)
    {
        GetType().GetProperty(eventName).SetValue(this, status);
    }

    // イベントの状態を取得するメソッド
    public EventStatus GetEventStatus(string eventName)
    {
        return (EventStatus)GetType().GetProperty(eventName).GetValue(this);
    }

    // 全てのイベント状態をリセットするメソッド
    public void ResetAllFlags()
    {
        foreach (var prop in GetType().GetProperties())
        {
            if (prop.PropertyType == typeof(EventStatus))
            {
                prop.SetValue(this, EventStatus.NotStarted);
            }
        }
    }
}