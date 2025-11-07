using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideClearCollider : MonoBehaviour
{
    [SerializeField] private ClearEffect clearEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.AreAllCardboardsInteracted())
            {
                clearEffect.PlayClearEffect();
            }
        }
    }
}
