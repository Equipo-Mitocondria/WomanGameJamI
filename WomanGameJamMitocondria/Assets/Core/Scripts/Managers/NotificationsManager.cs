using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    public static NotificationsManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
