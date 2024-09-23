using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textArea;

    public Transform player;           // プレイヤーのTransform
    public Vector3 offset;             // カメラとプレイヤーのオフセット

    public float sensitivity = 2.0f;

    private float verticalRotation = 0.0f;

    void Start()
    {
        if (player == null) return;

        offset = transform.position - player.position;  // 初期オフセット設定

        SyncCameraPosition();
    }

    void Update()
    {
        if (player == null) return;
        
        if(GameManager.instance.isCursor)
        {
            return;
        }

        // マウスによるカメラの上下操作
        float rotateVertical = -Input.GetAxis("Mouse Y") * sensitivity;
        verticalRotation += rotateVertical;
        verticalRotation = Mathf.Clamp(verticalRotation, -90.0f, 90.0f);

        // カメラの回転を適用
        transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);

        // カメラの位置をプレイヤーに追従
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