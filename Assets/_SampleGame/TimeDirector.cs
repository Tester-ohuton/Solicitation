using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class TimeData
{
    public float d_second;
    public int d_minute;
    public int d_hour;
}

public class TimeDirector : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    public float   second;
    public int     minute;
    public int     hour;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        second += Time.deltaTime;

        if (minute > 60)
        {
            hour++;
            minute = 0;
        }
        if (second > 60f)
        {
            minute += 1;
            second = 0;
        }

        timerText.text = "ÉfÅ[É^\n"+ hour.ToString() + ":" + minute.ToString("00") + ":" + second.ToString("f2");
    }

    public void SaveData()
    {
        TimeData data = new TimeData()
        {
            d_second = second,
            d_minute = minute,
            d_hour = hour
        };
        string json = JsonUtility.ToJson(data, true);
        Debug.Log(json);

        PlayerPrefs.SetString("PlayerTimeData", json);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("PlayerTimeData"))
        {
            string json = PlayerPrefs.GetString("PlayerTimeData");
            TimeData data = JsonUtility.FromJson<TimeData>(json);
            second = data.d_second;
            minute = data.d_minute;
            hour = data.d_minute;
        }
        else
        {
            Debug.Log("PlayerTimeDataÇ™ë∂ç›ÇµÇ‹ÇπÇÒ");
        }
    }
}