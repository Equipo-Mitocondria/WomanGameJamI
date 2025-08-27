using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.LookAt(CameraManager.Instance.ActiveCamera.transform.position);
    }
}