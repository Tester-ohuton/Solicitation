using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ׂ̕\���̈�𒲐�����
[ExecuteAlways]
public class AspectKeeper : MonoBehaviour
{
    [SerializeField] private Camera camera_;      // �Ώۂ̶��
    [SerializeField] private Vector2 aspectVec_;  // �Q�Ɖ𑜓x
    [SerializeField] private float pixelPerUnit_; // �摜��PixelPerUnit
    private float currentAspect_ = 0.0f;          // ���݂̱��߸Ĕ�

    void Update()
    {
        // ��ׂ̕\���̈�̒���
        SetCameraViewArea();
    }

    // ��ׂ̕\���̈�̒���
    private void SetCameraViewArea()
    {
        // ���݂̱��߸Ĕ���擾���A�ω����Ȃ���Ώ����𔲂���
        float currentAspect = (float)Screen.height / (float)Screen.width;

        if (Mathf.Approximately(currentAspect_, currentAspect))
        {
            return;
        }

        currentAspect_ = currentAspect;

        // ��׻��ނ̒���
        camera_.orthographicSize = aspectVec_.y / pixelPerUnit_ / 2;

        // ViewportRect�̒���
        SetViewportRect();
    }

    // ViewportRect�̒���
    private void SetViewportRect()
    {
        float baseAspect = aspectVec_.y / aspectVec_.x;     // ��̱��߸Ĕ�

        if (baseAspect > currentAspect_)
        {
            // ��ʂ����ɍL���ꍇ�A��̱��߸Ĕ�ɍ��킹�Ē�������
            float bgScale = aspectVec_.y / Screen.height;

            // viewportRect�̕�
            float tmpWidth = aspectVec_.x / (Screen.width * bgScale);

            // viewportRect��ݒ�
            camera_.rect = new Rect(
             (1.0f - tmpWidth) / 2, 0.0f, tmpWidth, 1.0f);
        }
        else
        {
            // ��ʂ��c�ɒ����ꍇ�A��ʂ̒����ɱ��߸Ĕ��Ǐ]������
            float bgScale = currentAspect_ / baseAspect;

            // ��ׂ̻��ނ��c�̒����ɍ��킹�Đݒ肵�Ȃ���
            camera_.orthographicSize *= bgScale;

            // viewportRect��ݒ�(�͈͓��S�Ă�\��)
            camera_.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        }
    }
}