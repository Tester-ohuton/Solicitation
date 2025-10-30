using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEffect : MonoBehaviour
{
    public ParticleSystem clearEffect;
    
    public void PlayClearEffect()
    {
        if (clearEffect != null)
        {
            clearEffect.Play();
        }
    }
}
