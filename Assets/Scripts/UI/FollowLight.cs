using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLight : MonoBehaviour
{
    Transform cameraTrans;
    [SerializeField] Transform playerTrans;
    [SerializeField] Vector3 cameraVec; // Vector3(0, 1, -1)
    [SerializeField] Vector3 cameraRot; // Vector3(45, 0, 0)

    private void Awake()
    {
        cameraTrans = transform;
        cameraTrans.rotation = Quaternion.Euler(cameraRot);
    }

    private void LateUpdate()
    {
        // カメラの位置をプレイヤーの位置に少し遅れて追従
        cameraTrans.position = Vector3.Lerp(cameraTrans.position, playerTrans.position + cameraVec, 2.0f * Time.deltaTime);
    }
}
