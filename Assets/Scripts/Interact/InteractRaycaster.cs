using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractRaycaster : MonoBehaviour
{
    [Header("Ray�̒���")]
    public float rayLength = 3f;

    [Header("Player")]
    [SerializeField] GameObject playerObject;

    [Header("ItemLists")]
    [SerializeField] GameObject itemListsObject;

    private GameObject transformObject;

    private void Update()
    {
        Intaract(transformObject);
        NonIntaract(transformObject);
    }

    public void Intaract(GameObject obj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    // �A�C�e�����v���C���[�̎q�I�u�W�F�N�g�Ɉړ�������
                    obj = hit.collider.gameObject;
                    // �A�C�e�����C���^���N�g���鏈��
                    if (obj == null)
                    {
                        obj.transform.SetParent(playerObject.transform);
                        obj.transform.position = new Vector3(0.5f, -0.5f, 1f); // �K�؂Ȉʒu�ɒ���
                    }
                }
            }
        }
    }

    public void NonIntaract(GameObject obj)
    {
        // �A�C�e����u������
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (obj != null)
            {
                // �A�C�e�����v���C���[�̎q�I�u�W�F�N�g�������
                obj.transform.SetParent(itemListsObject.transform);
                obj.transform.position = itemListsObject.transform.position; // �K�؂Ȉʒu�ɒ���
                obj = null;
            }
        }
    }

}