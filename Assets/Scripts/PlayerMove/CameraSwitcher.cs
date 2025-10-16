using UnityEngine;
using Cinemachine;
public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCameraBase playerMoverVcam1;
    public CinemachineVirtualCameraBase DoorIntaractVcam2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerMoverVcam1.Priority = 1;
            DoorIntaractVcam2.Priority = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerMoverVcam1.Priority = 0;
            DoorIntaractVcam2.Priority = 1;
        }
    }
}