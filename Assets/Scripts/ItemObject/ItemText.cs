using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemText : MonoBehaviour
{
    public static ItemText instance;

    public ParticleSystem clearEffect;

    public void PlayClearEffect(string effectName, Vector3 position, Quaternion rotation)
    {
        // エフェクトの再生処理をここに実装
        Debug.Log($"Playing effect: {effectName} at position {position} with rotation {rotation}");

        if (clearEffect != null)
        {
            clearEffect.Play();
        }
    }

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
            PlayClearEffect("ItemTextEffect", Vector3.zero, Quaternion.identity);
        });
    }
}
