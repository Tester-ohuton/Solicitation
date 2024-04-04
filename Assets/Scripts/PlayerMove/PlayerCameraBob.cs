using UnityEngine;

public class PlayerCameraBob : MonoBehaviour
{
    public float bobSpeed = 5f; // カメラの揺れ速度
    public float bobAmount = 0.05f; // カメラの揺れ量
    public float strideLength = 1f; // 歩幅（一定の距離）

    private Vector3 cameraOriginalPosition; // カメラの初期位置
    private float timer = 0f; // 時間計測用タイマー
    private bool isMoving = false; // プレイヤーが移動しているかどうかのフラグ

    void Start()
    {
        cameraOriginalPosition = transform.localPosition;
    }

    void Update()
    {
        PlayerLife.OnGameOver.AddListener(() =>
        {
            return;
        });

        if (!SceneFlagManager.Instance.isPlayerMoving)
            return;

        // プレイヤーが移動中かどうかを判定
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // プレイヤーが移動中であれば、カメラを揺らす
        if (isMoving)
        {
            timer += Time.deltaTime * bobSpeed;
            transform.localPosition = cameraOriginalPosition + new Vector3(0f, Mathf.Sin(timer) * bobAmount, 0f);
        }
        else
        {
            timer = 0f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, cameraOriginalPosition, Time.deltaTime * bobSpeed);
        }

        // 歩幅ごとにカメラの揺れをリセット
        if (Mathf.Abs(transform.localPosition.y - cameraOriginalPosition.y) >= strideLength)
        {
            timer = 0f;
            cameraOriginalPosition = transform.localPosition;
        }
    }
}