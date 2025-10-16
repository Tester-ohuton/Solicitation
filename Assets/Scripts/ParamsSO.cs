using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]//Createメニューから作成できるようする属性
public class ParamsSO : ScriptableObject
{
    //MyScriptableObjectが保存してある場所のパス
    public const string PATH = "ParamsSO";

    //MyScriptableObjectの実体
    private static ParamsSO _entity;
    public static ParamsSO Entity
    {
        get
        {
            //初アクセス時にロードする
            if (_entity == null)
            {
                _entity = Resources.Load<ParamsSO>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _entity;
        }
    }

    /*
    [Header("制限時間を止める")]
    [HideInInspector]
    public bool isStopTime;

    [Header("SaveFlag")]
    [HideInInspector]
    public bool isSaving;

    [Header("LoadFlag")]
    [HideInInspector]
    public bool isLoading;

    [Header("プレイヤーMoveFlag")]
    [HideInInspector]
    public bool isPlayerMoving;

    [Header("会話パートFlag")]
    [HideInInspector]
    public bool isTaking;

    [Header("設定画面が表示されていた場合にRayを飛ばさないFlag")]
    [HideInInspector]
    public bool isRayFalse;

    [Header("NormalEND")]
    [HideInInspector]
    public bool isNormalEnd;

    [Header("BadEND")]
    [HideInInspector]
    public bool isBadEnd;

    [Header("CardBoardOpenedFlag")]
    [HideInInspector]
    public List<int> isCardBoardOpened;

    [Space(50)]

    [Header("MouseSensitivity")]
    public float mouseSensitivity;

    [Header("Brightness")]
    public float brightness;
    */
}