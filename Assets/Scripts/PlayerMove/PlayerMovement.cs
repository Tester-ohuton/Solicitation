using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Slider mouseSensitivitySlider;

    [SerializeField] Camera cam;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] Transform viewPoint;
    [SerializeField] float mouseSensitivity = 1f;
    [SerializeField] Vector3 jumpForce = new Vector3(0, 6, 0);
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float rot = 75f;

    private Rigidbody rb;
    private Vector2 mouseInput;
    private float verticalMouseInput;

    private Vector3 moveDir;
    private Vector3 movement;
    private float activeMoveSpeed;

    // New variables for idle sensitivity reduction
    private float idleMouseSensitivity = 0.2f;
    private float mouseIdleTimer = 0f;
    private float idleThreshold = 1f;

    public void SetMouseSensitivity(float value)
    {
        mouseSensitivity = value;
    }

    void Start()
    {
        mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);

        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
    }

    void Update()
    {
        if (!PlayerController.instance.isPlayerMoving)
        {
            return;
        }

        PlayerMove();

        Run();

        PlayerRotate();

        //Jump();
    }

    public void PlayerRotate()
    {
        mouseInput = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y"));

        if (mouseInput.sqrMagnitude > 0.01f)
        {
            // Mouse has moved, reset the timer and use normal sensitivity
            mouseIdleTimer = 0f;
            mouseInput *= mouseSensitivity;
        }
        else
        {
            // Mouse has not moved, increment the idle timer
            mouseIdleTimer += Time.deltaTime;
            if (mouseIdleTimer >= idleThreshold)
            {
                // Reduce sensitivity if idle for longer than the threshold
                mouseInput *= idleMouseSensitivity;
            }
        }

        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + mouseInput.x,
            transform.eulerAngles.z);

        verticalMouseInput += mouseInput.y;
        verticalMouseInput = Mathf.Clamp(verticalMouseInput, -rot,rot);

        viewPoint.rotation = Quaternion.Euler(
            -verticalMouseInput,
            viewPoint.transform.rotation.eulerAngles.y,
            viewPoint.transform.rotation.eulerAngles.z);
    }

    private void LateUpdate()
    {
        if (!PlayerController.instance.isPlayerMoving)
        {
            return;
        }

        
        cam.transform.position = viewPoint.position;
        cam.transform.rotation = viewPoint.rotation;

        cinemachineVirtualCamera.transform.position = viewPoint.position;
        cinemachineVirtualCamera.transform.rotation = viewPoint.rotation;
        
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

    //public void Jump()
    //{
    //    if (IsGround() && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        rb.AddForce(jumpForce, ForceMode.Impulse);
    //    }
    //}

    //public bool IsGround()
    //{
    //    return Physics.Raycast(
    //        groundCheckPoint.position,
    //        Vector3.down,
    //        0.25f,
    //        groundLayer);
    //}

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

    public void UpdateCursorLock(bool cursor)
    {
        /*
         ê›íËÇäJÇ¢ÇΩèÛë‘ CursorLockMode.None;
         ëÄçÏê‡ñæÇäJÇ¢ÇΩèÛë‘ CursorLockMode.None;
         ï¬Ç∂ÇΩèÛë‘ 
         */

        if (cursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}