using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textArea;

    public Transform player;           // �v���C���[��Transform
    public Vector3 offset;             // �J�����ƃv���C���[�̃I�t�Z�b�g

    public float sensitivity = 2.0f;

    private float verticalRotation = 0.0f;

    void Start()
    {
        if (player == null) return;

        offset = transform.position - player.position;  // �����I�t�Z�b�g�ݒ�

        SyncCameraPosition();
    }

    void Update()
    {
        if (player == null) return;
        
        if(GameManager.instance.isCursor)
        {
            return;
        }

        // �}�E�X�ɂ��J�����̏㉺����
        float rotateVertical = -Input.GetAxis("Mouse Y") * sensitivity;
        verticalRotation += rotateVertical;
        verticalRotation = Mathf.Clamp(verticalRotation, -90.0f, 90.0f);

        // �J�����̉�]��K�p
        transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);

        // �J�����̈ʒu���v���C���[�ɒǏ]
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = targetPosition;
    }

    void SyncCameraPosition()
    {
        // Update camera position based on player's current position
        transform.position = player.position + offset;
    }
}