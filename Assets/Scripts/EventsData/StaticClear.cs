using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class StaticClear : MonoBehaviour
{
    public static StaticClear Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ステージ番号
    public static int StageNo;

    // ステージ数
    public static int StageCount = 0;

    public static int GetStageNo()
    {
        return StageNo;
    }

    public static int GetStageCount()
    {
        return StageCount;
    }

    public static void AddStageCount()
    {
        StageCount++;
    }

    public static void ClearStatics()
    {
        StaticClear[] staticClears = FindObjectsOfType<StaticClear>();
        foreach (StaticClear sc in staticClears)
        {
            Destroy(sc.gameObject);
        }
    }
}
