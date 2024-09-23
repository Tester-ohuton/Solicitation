using UnityEngine;

public class PlayerCameraBob : MonoBehaviour
{
    public float bobSpeed = 5f; // �J�����̗h�ꑬ�x
    public float bobAmount = 0.05f; // �J�����̗h���
    public float strideLength = 1f; // �����i���̋����j

    private Vector3 cameraOriginalPosition; // �J�����̏����ʒu
    private float timer = 0f; // ���Ԍv���p�^�C�}�[
    private bool isMoving = false; // �v���C���[���ړ����Ă��邩�ǂ����̃t���O

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

        if (!PlayerController.instance.isPlayerMoving)
            return;

        // �v���C���[���ړ������ǂ����𔻒�
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // �v���C���[���ړ����ł���΁A�J������h�炷
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

        // �������ƂɃJ�����̗h������Z�b�g
        if (Mathf.Abs(transform.localPosition.y - cameraOriginalPosition.y) >= strideLength)
        {
            timer = 0f;
            cameraOriginalPosition = transform.localPosition;
        }
    }
}