using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]//Create���j���[����쐬�ł���悤���鑮��
public class ParamsSO : ScriptableObject
{
    //MyScriptableObject���ۑ����Ă���ꏊ�̃p�X
    public const string PATH = "ParamsSO";

    //MyScriptableObject�̎���
    private static ParamsSO _entity;
    public static ParamsSO Entity
    {
        get
        {
            //���A�N�Z�X���Ƀ��[�h����
            if (_entity == null)
            {
                _entity = Resources.Load<ParamsSO>(PATH);

                //���[�h�o���Ȃ������ꍇ�̓G���[���O��\��
                if (_entity == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _entity;
        }
    }

    /*
    [Header("�������Ԃ��~�߂�")]
    [HideInInspector]
    public bool isStopTime;

    [Header("SaveFlag")]
    [HideInInspector]
    public bool isSaving;

    [Header("LoadFlag")]
    [HideInInspector]
    public bool isLoading;

    [Header("�v���C���[MoveFlag")]
    [HideInInspector]
    public bool isPlayerMoving;

    [Header("��b�p�[�gFlag")]
    [HideInInspector]
    public bool isTaking;

    [Header("�ݒ��ʂ��\������Ă����ꍇ��Ray���΂��Ȃ�Flag")]
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