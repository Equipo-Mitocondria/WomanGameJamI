using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    public static NotificationsManager Instance;
    [SerializeField] Sanity _sanity;
    [SerializeField] float _notificationPeriod;
    [SerializeField] float _notificationChance;
    
    private Notification[] notifications;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //string[] rows = Parser(path);
        //CreateNotifications(rows);

        StartCoroutine(NotificationPeriod());
    }

    private Notification[] CreateNotifications(string[] rows)
    {
        //TO DO
        return null;
    }

    private IEnumerator NotificationPeriod()
    {
        if (WillNotificationSpawn())
            NotificationSpawn();

        yield return new WaitForSeconds(_notificationPeriod);

        StartCoroutine(NotificationPeriod());
    }

    private bool WillNotificationSpawn()
    {
        if(Random.Range(0f, 1f) <= _notificationChance)
            return true;
        else
            return false;
    }
    
    private void NotificationSpawn()
    {
        Notification notification = GetRandomNotification();

        UIManager.Instance.ShowNotificationPopUp(notification.message, notification.speakerName, notification.avatar);
        AudioManager.Instance.PlayNotificationPopUp();

        _sanity.ApplySanityEffect(notification.sanityEffect);
    }

    private Notification GetRandomNotification()
    {
        Notification notification = notifications[Random.Range(0, notifications.Length - 1)];

        return notification;
    }
}
