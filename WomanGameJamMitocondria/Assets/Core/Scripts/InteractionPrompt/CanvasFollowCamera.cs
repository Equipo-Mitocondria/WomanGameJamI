using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(CameraManager.Instance.ActiveCamera.transform.position);
    }
}