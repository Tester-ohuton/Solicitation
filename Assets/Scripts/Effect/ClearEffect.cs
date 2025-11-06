using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEffect : MonoBehaviour
{
    public GameObject clearEffect;
    
    public void PlayClearEffect()
    {
        clearEffect.SetActive(true);
    }
}
