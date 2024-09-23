using System.Collections.Generic;
using UnityEngine;

// 各イベントの状態を定義
public enum EventStatus
{
    NotStarted,
    InProgress,
    Completed
}

// 各イベントのクラス
[System.Serializable]
public class GameEvent
{
    public string description;
    public EventStatus status = EventStatus.NotStarted;
    public System.Action onStart;
    public System.Action onComplete;

    public void StartEvent()
    {
        status = EventStatus.InProgress;
        onStart?.Invoke();
    }

    public void CompleteEvent()
    {
        status = EventStatus.Completed;
        onComplete?.Invoke();
    }
}

// 特定の日付のイベントを保持するクラス
[System.Serializable]
public class DayEvents
{
    public string dayDescription;
    public List<GameEvent> events;
}

// 全体のイベント管理クラス
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [Header("各日付のイベントリスト")]
    public List<DayEvents> dayEventsList;
    private int currentDay = 0;
    private int currentEvent = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartNextEvent();
    }

    public string CurrentDayDescription => currentDay < dayEventsList.Count ? dayEventsList[currentDay].dayDescription : "No more days";
    public string CurrentEventDescription => currentDay < dayEventsList.Count && currentEvent < dayEventsList[currentDay].events.Count ? dayEventsList[currentDay].events[currentEvent].description : "No more events";

    public int CurrentDay => currentDay;

    public void StartNextEvent()
    {
        if (currentDay < dayEventsList.Count)
        {
            DayEvents dayEvents = dayEventsList[currentDay];
            if (currentEvent < dayEvents.events.Count)
            {
                GameEvent gameEvent = dayEvents.events[currentEvent];
                gameEvent.StartEvent();
            }
        }
    }

    public void CompleteCurrentEvent()
    {
        if (currentDay < dayEventsList.Count)
        {
            DayEvents dayEvents = dayEventsList[currentDay];
            if (currentEvent < dayEvents.events.Count)
            {
                GameEvent gameEvent = dayEvents.events[currentEvent];
                gameEvent.CompleteEvent();
                currentEvent++;
                if (currentEvent >= dayEvents.events.Count)
                {
                    currentDay++;
                    currentEvent = 0;
                }
                StartNextEvent();
            }
        }
    }
}