using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    private void Awake()
    {
        instance = this;
    }

    Camera subCamera = default;
    Camera mainCamera = default;

    public void ChangeCamera(Camera mainCamera,Camera subCamera)
    {
        this.mainCamera = mainCamera;
        this.subCamera = subCamera;
        this.mainCamera.gameObject.SetActive(false);
        this.subCamera.gameObject.SetActive(true);
        SceneFlagManager.Instance.isPlayerMoving = false;
    }


    public void OnBackCamera()
    {
        this.subCamera.gameObject.SetActive(false);
        this.mainCamera.gameObject.SetActive(true);
        SceneFlagManager.Instance.isPlayerMoving = true;
    }
}