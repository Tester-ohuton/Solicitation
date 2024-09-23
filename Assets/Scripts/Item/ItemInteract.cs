using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 ItemInteract.cs
�A�C�e���Ǘ��N���X
 */
public class ItemInteract : MonoBehaviour
{
    public static UnityEvent onInteract = new UnityEvent();
    public static UnityEvent onNonInteract = new UnityEvent();

    [Header("�v���C���[�ɒǏ]����")]
    public GameObject player;
    
    [Header("�v���C���[�̎q�I�u�W�F�N�g")]
    public GameObject itemPos;

    [Header("Item")]
    public GameObject itemObject;

    [Header("ItemRoot")]
    public Transform itemRoot;

    private void Start()
    {
        Position();
        PlayerPosition();

        Instantiate(itemObject,Position(),Quaternion.identity, itemRoot);
    }

    private void Update()
    {
        Position();
        PlayerPosition();
    }

    public Vector3 Position() // ���̈ʒu
    {
        return transform.position;
    }

    public Vector3 PlayerPosition() // �C���^���N�g�̈ʒu
    {
        Vector3 position = 
            player.transform.position +
            itemPos.transform.position;
        return position;
    }
}