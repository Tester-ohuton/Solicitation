using System.Collections.Generic;
using UnityEngine;

public class SceneFlagManager : MonoBehaviour
{
    public static SceneFlagManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Header("SaveFlag")]
    public bool isSaving;

    [Header("LoadFlag")]
    public bool isLoading;

    [Header("プレイヤーMoveFlag")]
    public bool isPlayerMoving;

    [Header("会話パートFlag")]
    public bool isTaking;

    [Header("Debug用時間を止めるFlag")]
    public bool isStopTime;

    [Header("設定画面が表示されていた場合にRayを飛ばさないFlag")]
    public bool isSetting;

    [Header("NormalEND")]
    public bool isNormalEnd;

    [Header("BadEND")]
    public bool isBadEnd;

    [Header("DayFlag")]
    public bool[] isDay;

    [Header("CardBoardOpenedFlag")]
    public bool[] isCardBoardOpened;

    [Header("Chime")]
    public bool isChime;
}