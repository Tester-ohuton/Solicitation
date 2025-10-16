using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ItemText : MonoBehaviour
{
    public static ItemText instance;

    public static UnityEvent onItemText = new UnityEvent();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        onItemText.RemoveAllListeners();

        onItemText.AddListener(() =>
        {
            // クリアエフェクト
            GlobalVolume.instance.ShowItemObtainedEffect(0.4f, 1f);
        });
    }
}
