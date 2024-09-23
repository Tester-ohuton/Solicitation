using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ItemText : MonoBehaviour
{
    public static ItemText instance;

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
            // �N���A�G�t�F�N�g
            GlobalVolume.instance.ShowItemObtainedEffect(0.4f, 1f);
        });
    }
}
