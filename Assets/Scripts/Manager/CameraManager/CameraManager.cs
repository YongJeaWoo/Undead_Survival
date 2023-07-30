using Cinemachine;
using SingletonComponent.Component;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : SingletonComponent<CameraManager>
{
    private Camera mainCamera;
    private CinemachineBrain bCamera;

    #region Singleton

    protected override void AwakeInstance()
    {
        mainCamera = Camera.main;
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
    }

    #endregion

    public Camera MainCamera() => mainCamera;

    public void InitCamera()
    {
        bCamera = mainCamera.AddComponent<CinemachineBrain>();
        bCamera.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
        GameObject newCam = new GameObject("VirtualCamera");
        newCam.transform.position = new Vector3(0, 0, -10);
        CinemachineVirtualCamera vCamera = newCam.AddComponent<CinemachineVirtualCamera>();
        vCamera.Follow = PlayerManager.Instance.GetPlayer().transform;
        vCamera.m_Lens.OrthographicSize = 4f;
        vCamera.AddCinemachineComponent<CinemachineTransposer>();
    }
}
