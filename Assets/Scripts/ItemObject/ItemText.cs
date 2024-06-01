using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ItemText : MonoBehaviour
{
    public static ItemText instance;

    [HideInInspector] public int value;
    public bool openNumber;

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
    }

    private void Update()
    {
        itemText.text = "Item: " + value + "/6";
    }

    private void SetItemValue()
    {
        value += 1;
        SceneFlagManager.Instance.isCardBoardOpened.Add(ItemLogger.Count);
    }
}
