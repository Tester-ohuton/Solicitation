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

    [Header("SceneFlagManagerのゲーム内機能を有効にするかしないか")]
    public bool isActive;

    [Header("Lists")]
    public List<Sun> sunLists = new List<Sun>();

}

[System.Serializable]
public class Sun
{
    public enum SUN
    {
        AGE_1,
        AGE_2,
        AGE_3,
        AGE_4,
        AGE_5,
    }

    [Header("Tag")]
    public SUN sun;

    [Header("何日目か")]
    public bool isAGE; // 日ごとのisAGE情報

    // 各日に異なる部屋に変化を起こすフラグのリスト
    [Header("部屋に変化を起こすフラグ")]
    public List<RoomFlag> roomFlags = new List<RoomFlag>();
}

[System.Serializable]
public class RoomFlag
{
    [Header("部屋の異変を起こすフラグ")]
    public bool isActive;
}