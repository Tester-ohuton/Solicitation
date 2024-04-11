using System.Collections.Generic;
using UnityEngine;

public class SceneFlagManager : MonoBehaviour
{
    public static SceneFlagManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Header("SaveFlag")]
    public bool isSaving;

    [Header("LoadFlag")]
    public bool isLoading;

    [Header("�v���C���[MoveFlag")]
    public bool isPlayerMoving;

    [Header("��b�p�[�gFlag")]
    public bool isTaking;

    [Header("Debug�p���Ԃ��~�߂�Flag")]
    public bool isStopTime;

    [Header("�ݒ��ʂ��\������Ă����ꍇ��Ray���΂��Ȃ�Flag")]
    public bool isSetting;

    [Header("SceneFlagManager�̃Q�[�����@�\��L���ɂ��邩���Ȃ���")]
    public bool isActive;


}