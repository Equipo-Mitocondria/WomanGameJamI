using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Cameras _cameraToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player"))
            return;

        if(CameraManager.Instance != null)
        {
            CameraManager.Instance.ChangeCamera(_cameraToTrigger);
        }
    }
}
