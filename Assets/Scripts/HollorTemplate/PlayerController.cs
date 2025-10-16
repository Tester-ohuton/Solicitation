using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [TextArea(1, 10)]
    public string textArea;

    public float moveSpeed = 5f;
    public float turnSpeed = 720f;

    public float sensitivity = 0.1f;
    public float mouseSensitivity = 1f;    //マウス感度
    public Slider mouseSlider;

    [Header("Crouch Settings")]
    public float crouchHeight = 1.0f;

    [Header("Key Settings")]
    public KeyCode crouchKey = KeyCode.LeftShift;//shiftキー

    [Header("Camera")]
    public Transform cameraTransform; //カメラのTransform

    public bool isPlayerMoving;

    private Rigidbody rb;
    private Vector3 velocity;
    private CapsuleCollider capsuleCollider;
    private float originalHeight;
    private float verticalRotation = 0.0f;

    void Start()
    {
        mouseSensitivity = GameManager.instance.mouseSensitivity;

        if (mouseSlider == null) return;

        mouseSlider.value = mouseSensitivity;

        mouseSlider.onValueChanged.AddListener((value) =>
        {
            SetMouseSencitivity(value);
        });

        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalHeight = capsuleCollider.height;

        // Set player position based on saved data
        transform.position = GameManager.instance.playerPosition;
        rb.position = GameManager.instance.playerPosition;
    }

    void Update()
    {
        if(!isPlayerMoving)
        {
            return;
        }

        GameManager.instance.UpdateGameTime(Time.deltaTime);
        UIManager.instance.UpdateGameTimeUI(GameManager.instance.GetFormattedGameTime());

        ActorMove();
        Rotate();
        Crouch();
        StandUp();
    }

    /// <summary>
    /// プレイヤー移動
    /// </summary>
    void ActorMove()
    {

        //プレイヤー移動
        float moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float moveVertical = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(moveHorizontal, 0, moveVertical);

        // Save the player position to GameManager
        GameManager.instance.playerPosition = transform.position;
    }

    /// <summary>
    /// マウス視点操作
    /// </summary>
    void Rotate()
    {
        //マウス視点操作
        float rotateHorizontal = Input.GetAxis("Mouse X") * sensitivity * mouseSensitivity;
        transform.Rotate(0, rotateHorizontal, 0);
    }

    /// <summary>
    /// しゃがむ
    /// </summary>
    void Crouch()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            moveSpeed = 2.5f; // しゃがみ時の"移動速度"
            capsuleCollider.height = crouchHeight;
            capsuleCollider.center = new Vector3(capsuleCollider.center.x, crouchHeight / 2, capsuleCollider.center.z);
        }
    }

    /// <summary>
    /// 立ち上がる
    /// </summary>
    void StandUp()
    {
        if (Input.GetKeyUp(crouchKey))
        {
            moveSpeed = 5f; // 元の移動速度
            capsuleCollider.height = originalHeight;
            capsuleCollider.center = new Vector3(capsuleCollider.center.x, originalHeight / 2, capsuleCollider.center.z);
        }
    }

    /// <summary>
    /// マウス感度を設定
    /// </summary>
    /// <param name="intensity"></param>
    public void SetMouseSencitivity(float intensity)
    {
        mouseSensitivity = intensity;
    }
}