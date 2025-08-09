using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    public static NotificationsManager Instance;
    [SerializeField] Sanity _sanity;
    [SerializeField] float _notificationPeriod;
    [SerializeField] float _notificationChance;
    
    private NotificationNode[] notifications;
    NotificationsBST notificationsBST;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        string path = "";
        int levelID = 1;

        string csv = CSVImporter.ImportCSV(path);
        List<string[]> parsedCSV = CSVParser.ParseCSV(csv);
        notificationsBST = new NotificationsBST(NotificationsBuilder.BuildNotificationListsList(parsedCSV, levelID));

        StartCoroutine(NotificationPeriod());
    }

    private List<NotificationNode> GetNotificationList(int id)
    {
        return notificationsBST.Search(id);
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
        NotificationNode notification = GetRandomNotification();

        //UIManager.Instance.ShowNotificationPopUp(notification.message, notification.avatar);
        AudioManager.Instance.PlayNotificationPopUp();

        _sanity.ApplySanityEffect(notification.sanityEffect);
    }

    private NotificationNode GetRandomNotification()
    {
        NotificationNode notification = notifications[Random.Range(0, notifications.Length - 1)];

        return notification;
    }
}
