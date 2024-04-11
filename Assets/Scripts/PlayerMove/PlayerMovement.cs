using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform viewPoint;
    [SerializeField] float mouseSensitivity = 1f;
    [SerializeField] Vector3 jumpForce = new Vector3(0, 6, 0);
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 8f;

    private Rigidbody rb;
    private Vector2 mouseInput;
    private float verticalMouseInput;
    private Camera cam;

    private Vector3 moveDir;
    private Vector3 movement;
    private float activeMoveSpeed;
    private bool cursorLock = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
        cam = Camera.main;

        if (!SceneFlagManager.Instance.isPlayerMoving)
            return;

        UpdateCursorLock();
    }

    void Update()
    {
        if (!SceneFlagManager.Instance.isPlayerMoving)
            return;

        PlayerRotate();

        PlayerMove();

        Run();

        Jump();

        UpdateCursorLock();
    }

    public void PlayerRotate()
    {
        mouseInput = new Vector2(
            Input.GetAxisRaw("Mouse X") * mouseSensitivity,
            Input.GetAxisRaw("Mouse Y") * mouseSensitivity);

        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + mouseInput.x,
            transform.eulerAngles.z);

        verticalMouseInput += mouseInput.y;
        verticalMouseInput = Mathf.Clamp(verticalMouseInput, -60f, 60f);

        viewPoint.rotation = Quaternion.Euler(
            -verticalMouseInput,
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);
    }

    private void LateUpdate()
    {
        cam.transform.position = viewPoint.position;
        cam.transform.rotation = viewPoint.rotation;
    }

    public void PlayerMove()
    {
        moveDir = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical"));

        movement =
            ((transform.forward * moveDir.z) +
            (transform.right * moveDir.x)).normalized;

        transform.position += movement * activeMoveSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        if (IsGround() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    public bool IsGround()
    {
        return Physics.Raycast(
            groundCheckPoint.position,
            Vector3.down,
            0.25f,
            groundLayer);
    }

    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = runSpeed;
        }
        else
        {
            activeMoveSpeed = walkSpeed;
        }
    }

    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}