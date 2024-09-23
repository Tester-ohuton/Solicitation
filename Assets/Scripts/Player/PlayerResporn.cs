using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResporn : MonoBehaviour
{
    [Header("TriggerCheck")]
    [SerializeField] TriggerCheck[] triggerCheck;

    bool resporn = false;

    private void Update()
    {
        if (resporn)
        {
            resporn = false;
            return;
        }

        for (int i = 0; i < triggerCheck.Length; i++)
        {
            if (triggerCheck[i].isOK)
            {
                GameManager.instance.ResetPlayerPos();
                resporn = true;
            }
        }
    }
}
