using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    public static NotificationsManager Instance;
    [SerializeField] float _notificationPeriod;
    [SerializeField] float _notificationChance;
    
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
        string path = "Assets/Resources/Notifications CSVs/notifications-example.csv";
        int levelID = 1;

        string csv = CSVImporter.ImportCSV(path);
        List<string[]> parsedCSV = CSVParser.ParseCSV(csv);
        notificationsBST = new NotificationsBST(NotificationsBuilder.BuildNotificationListsList(parsedCSV, levelID));

        StartCoroutine(NotificationPeriod());
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
        List<NotificationNode> notificationNodes = GetRandomNotificationNodeList();

        UIManager.Instance.StartNotificationThread(notificationNodes);
        AudioManager.Instance.PlayNotificationPopUp();

        NotificationNode first = notificationNodes.FirstOrDefault();
        Sanity.Instance.ApplySanityEffect(first.sanityEffect);
    }

    private List<NotificationNode> GetRandomNotificationNodeList()
    {
        int randomId = Random.Range(0, notificationsBST.Count - 1);
        List<NotificationNode> notificationNodes = GetNotificationList(randomId);

        return notificationNodes;
    }

    private List<NotificationNode> GetNotificationList(int id)
    {
        return notificationsBST.Search(id);
    }
}
