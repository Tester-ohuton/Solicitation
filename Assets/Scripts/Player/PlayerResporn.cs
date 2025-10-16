using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResporn : MonoBehaviour
{
    [Header("TriggerCheck")]
    [SerializeField] TriggerCheck[] triggerCheck;

    public GameObject endPosition;

    bool resporn = false;
    int i = 0;

    private void Update()
    {
        if (resporn)
        {
            resporn = false;
            return;
        }

        for(i = 0; i < triggerCheck.Length; i++)
        {
            // 外壁に触れたら
            if (triggerCheck[i].isOK)
            {
                //プレイヤーを初期位置に再配置
                transform.position = endPosition.transform.position;
                resporn = true;

                break;
            }
        }
    }
}
