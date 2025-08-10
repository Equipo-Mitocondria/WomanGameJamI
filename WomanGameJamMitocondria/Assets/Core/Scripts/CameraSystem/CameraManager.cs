using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private Cameras _activeCamera;

    [SerializeField] private CinemachineVirtualCameraBase[] _cameras;

    public CinemachineVirtualCameraBase ActiveCamera { get { return GetActiveCinemachineCamera(_activeCamera); } }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SetActiveCamera(GetActiveCinemachineCamera(_activeCamera));
    }

    public void ChangeCamera(Cameras camera)
    {
        switch (camera)
        {
            case Cameras.Kitchen:
                SetActiveCamera(_cameras[0]);
                break;
            case Cameras.Living:
                SetActiveCamera(_cameras[1]);
                break;
            case Cameras.Desk:
                SetActiveCamera(_cameras[2]);
                break;
            case Cameras.Bedroom:
                SetActiveCamera(_cameras[3]);
                break;
            case Cameras.Bathroom:
                SetActiveCamera(_cameras[4]);
                break;
            default:
                throw new System.Exception("Camera not found.");
        }
    }

    private CinemachineVirtualCameraBase GetActiveCinemachineCamera(Cameras cameraToGet)
    {
        switch (cameraToGet)
        {
            case Cameras.Kitchen:
                return _cameras[0];
            case Cameras.Living:
                return _cameras[1];
            case Cameras.Desk:
                return _cameras[2];
            case Cameras.Bedroom:
                return _cameras[3];
            case Cameras.Bathroom:
                return _cameras[4];
            default:
                throw new System.Exception("Camera not found.");
        }
    }

    private Cameras GetActiveCamera(CinemachineVirtualCameraBase camToGet)
    {
        switch (camToGet)
        {
            case CinemachineVirtualCameraBase cam when cam == _cameras[0]:
                return Cameras.Kitchen;
            case CinemachineVirtualCameraBase cam when cam == _cameras[1]:
                return Cameras.Living;
            case CinemachineVirtualCameraBase cam when cam == _cameras[2]:
                return Cameras.Desk;
            case CinemachineVirtualCameraBase cam when cam == _cameras[3]:
                return Cameras.Bedroom;
            case CinemachineVirtualCameraBase cam when cam == _cameras[4]:
                return Cameras.Bathroom;
            default:
                throw new System.Exception("Camera not found.");
        }
    }

    private void SetActiveCamera(CinemachineVirtualCameraBase cameraToActivate)
    {
        foreach (CinemachineVirtualCameraBase cam in _cameras)
        {
            if (cam == cameraToActivate)
            {
                _activeCamera = GetActiveCamera(cam);
                cam.Priority = 50;
            }
            else
                cam.Priority = 0;
        }
    }
}
