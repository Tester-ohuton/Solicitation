using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ItemText : MonoBehaviour
{
    public static ItemText instance;

    public int value;
    public bool[] openNumber;

    public bool isComp;

    [SerializeField] TextMeshProUGUI itemText;

    public static UnityEvent OnItemText = new UnityEvent();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        OnItemText.RemoveAllListeners();
        OnItemText.AddListener(() =>
        {
            SetItemValue();
            
            // クリアエフェクト
            GlobalVolume.instance.ShowItemObtainedEffect();
            
            // クリアエフェクト
            // GlobalVolume.instance.ShowIllumination();
        });
    }

    private void Start()
    {
        itemText.text = "Item: " + value + "/6";
        isComp = false;
    }

    private void Update()
    {
        itemText.text = "Item: " + value + "/6";

        if (ItemLogger.Count == 2)
        {
            Debug.Log("Two");
        }
        if (ItemLogger.Count == 5)
        {
            Debug.Log("Five");
        }

        if (6 <= value)
        {
            isComp = true;
            Debug.Log($"isComp{isComp}");
        }
    }

    private void SetItemValue()
    {
        value += 1;
    }
}
