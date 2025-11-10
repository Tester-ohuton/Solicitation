using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public enum EventStatus
{
    NotStarted,
    InProgress,
    Completed
}

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

[System.Serializable]
public class DayEvents
{
    public string dayDescription;
    public List<GameEvent> events;
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [Header("UI Elements")]
    [SerializeField] private Text dayDescriptionText;
    [SerializeField] private Text eventDescriptionText;
    [SerializeField] private GameObject uiPanel; // UI をまとめて非表示にするパネル（Inspectorで割り当ててください）
    [SerializeField] private float typingSpeed = 0.03f; // 文字送りの速度（秒）

    [Header("日ごとのイベントリスト")]
    public List<DayEvents> dayEventsList;
    private int currentDay = 0;
    private int currentEvent = 0;

    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // 初期表示
        UpdateDayDescription();
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
                // 次のイベント開始時は UI を表示して文字送りを行う（閉じない）
                StartCoroutine(TypeText(gameEvent.description, true, false));
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
                // イベントが完了したら、その説明を文字送りで表示し、
                // 文字送りが完了したタイミングで UI を閉じ、続けて次のイベントへ進める

                // リストのインデックスごとに表示非表示を切り替える
                StartCoroutine(HandleEventCompletionAndAdvance(gameEvent));
            }
        }
    }

    // 完了処理を行い、文字送りが終わるまで待ってからインデックスを進める
    private IEnumerator HandleEventCompletionAndAdvance(GameEvent completedEvent)
    {
        // 文字送りを実行し完了を待つ（最後に UI を閉じる）
        yield return StartCoroutine(TypeText(completedEvent.description, true, true));

        // イベントのカウントを進める（文字送り完了後に進める）
        if (currentDay < dayEventsList.Count)
        {
            DayEvents dayEvents = dayEventsList[currentDay];
            currentEvent++;
            if (currentEvent >= dayEvents.events.Count)
            {
                currentDay++;
                currentEvent = 0;
                UpdateDayDescription();
            }
        }

        //// 次のイベントがあれば開始する（StartNextEvent は必要に応じて UI を再表示する）
        //StartNextEvent();
    }

    private void UpdateDayDescription()
    {
        if (dayDescriptionText != null && currentDay < dayEventsList.Count)
        {
            dayDescriptionText.text = dayEventsList[currentDay].dayDescription;
        }
    }

    // 文字送りを行うコルーチン
    private IEnumerator TypeText(string description, bool showUI, bool closeWhenDone)
    {
        // 既存の文字送りがあれば停止
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        showUI = true;

        // UI を表示する場合はパネルを有効化
        if (uiPanel != null)
        {
            uiPanel.SetActive(showUI);
        }
        else
        {
            // uiPanel が未設定でもテキストは表示されるようにしておく
            if (eventDescriptionText != null)
                eventDescriptionText.gameObject.SetActive(showUI);
        }

        if (eventDescriptionText == null)
            yield break;

        eventDescriptionText.text = "";

        for (int i = 0; i < description.Length; i++)
        {
            eventDescriptionText.text += description[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        // 文字送り完了時に UI を閉じる指定があれば閉じる
        if (closeWhenDone)
        {
            CloseUI();
        }

        typingCoroutine = null;
    }

    private void CloseUI()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
        else
        {
            if (eventDescriptionText != null)
                eventDescriptionText.gameObject.SetActive(false);
            if (dayDescriptionText != null)
                dayDescriptionText.gameObject.SetActive(false);
        }
    }

    // デバッグ用: キー入力でイベントを完了させる
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CompleteCurrentEvent();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            // 次のイベントがあれば開始する（StartNextEvent は必要に応じて UI を再表示する）
            StartNextEvent();
        }
    }
}