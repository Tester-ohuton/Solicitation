using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ItemText : MonoBehaviour
{
    public static ItemText instance;

    public int value;

    public bool isOne;
    public bool isTwo;
    public bool isThree;
    public bool isFour;
    public bool isFive;
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
            
            //アイテム取得エフェクト
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

        if (1 <= value)
        {
            isOne = true;
        }

        if (2 <= value)
        {
            isTwo = true;
            Debug.Log($"isTwo{isTwo}");
        }

        if (3 <= value)
        {
            isThree = true;
            Debug.Log($"isThree{isThree}");
        }

        if (4 <= value)
        {
            isFour = true;
            Debug.Log($"isFour{isFour}");
        }

        if (5 <= value)
        {
            isFive = true;
            Debug.Log($"isFive{isFive}");
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
