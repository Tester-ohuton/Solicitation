using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textArea;

    public GameObject[] anomalies;

    public void TriggerAnomaly(int index)
    {
        if (index >= 0 && index < anomalies.Length)
        {
            anomalies[index].SetActive(true);
        }
    }

    public void ResetAnomalies()
    {
        foreach (GameObject anomaly in anomalies)
        {
            anomaly.SetActive(false);
        }
    }
}