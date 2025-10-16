using System.Collections.Generic;
using UnityEngine;
using TMPro;

// �e�C�x���g�̏�Ԃ��`
public enum EventStatus
{
    NotStarted,
    InProgress,
    Completed
}

// �e�C�x���g�̃N���X
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

// ����̓��t�̃C�x���g��ێ�����N���X
[System.Serializable]
public class DayEvents
{
    public string dayDescription;
    public List<GameEvent> events;
}

// �S�̂̃C�x���g�Ǘ��N���X
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI dayDescriptionText;
    [SerializeField] private TextMeshProUGUI eventDescriptionText;

    [Header("�e���t�̃C�x���g���X�g")]
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
        UpdateDayDescription();
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
                UpdateEventDescription(gameEvent.description);
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
                    UpdateDayDescription();
                }
                StartNextEvent();
            }
        }
    }

    private void UpdateDayDescription()
    {
        if (dayDescriptionText != null && currentDay < dayEventsList.Count)
        {
            dayDescriptionText.text = dayEventsList[currentDay].dayDescription;
        }
    }

    private void UpdateEventDescription(string description)
    {
        if (eventDescriptionText != null)
        {
            eventDescriptionText.text = description;
        }
    }
}