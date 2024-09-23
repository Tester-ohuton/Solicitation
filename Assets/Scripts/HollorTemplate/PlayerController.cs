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

    public float sensitivity = 2.0f;
    public float mouseSensitivity = 1f;    // ���_����̊��x
    public Slider mouseSlider;

    [Header("���Ⴊ�񂾂Ƃ��̍���")]
    public float crouchHeight = 1.0f; // ���Ⴊ�񂾂Ƃ��̍���

    [Header("�_�b�V��")]
    public KeyCode crouchKey = KeyCode.C; // ���Ⴊ�݃L�[

    [Header("Camera")]
    public Transform cameraTransform; // �v���C���[�Ɋ֘A�t����ꂽ�J����

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
        // �X���C�_�[��G������}�E�X���x���ω�����
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

        //Move();

        ActorMove();
        Rotate();
        Crouch();
        StandUp();
    }

    void FixedUpdate()
    {
    }

    void Move()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

        // Save the player position to GameManager
        GameManager.instance.playerPosition = transform.position;
    }

    void ActorMove()
    {

        // �v���C���[�̈ړ�
        float moveHorizontal = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float moveVertical = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(moveHorizontal, 0, moveVertical);

        // �}�E�X�ɂ�鎋�_����i�������j
        float rotateHorizontal = Input.GetAxis("Mouse X") * sensitivity * mouseSensitivity;
        transform.Rotate(0, rotateHorizontal, 0);

        // Save the player position to GameManager
        GameManager.instance.playerPosition = transform.position;
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }

    void Crouch()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            moveSpeed = 2.5f; // ���Ⴊ�ݎ��̈ړ����x
            capsuleCollider.height = crouchHeight;
            capsuleCollider.center = new Vector3(capsuleCollider.center.x, crouchHeight / 2, capsuleCollider.center.z);
        }
    }

    void StandUp()
    {
        if (Input.GetKeyUp(crouchKey))
        {
            moveSpeed = 5f; // �ʏ�̈ړ����x
            capsuleCollider.height = originalHeight;
            capsuleCollider.center = new Vector3(capsuleCollider.center.x, originalHeight / 2, capsuleCollider.center.z);
        }
    }

    public void SetMouseSencitivity(float intensity)
    {
        mouseSensitivity = intensity;
    }
}