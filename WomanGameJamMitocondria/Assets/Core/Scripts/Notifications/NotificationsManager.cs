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
        string path = Application.streamingAssetsPath + "/Notifications.csv";

        string csv = CSVImporter.ImportCSV(path);
        List<string[]> parsedCSV = CSVParser.ParseCSV(csv);
        notificationsBST = new NotificationsBST(NotificationsBuilder.BuildNotificationListsList(parsedCSV, (int)GameManager.CurrentPhase));

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

        NotificationNode first = notificationNodes.FirstOrDefault();
        Sanity.Instance.ApplySanityEffect(first.sanityEffect);

        UIManager.Instance.StartNotificationThread(notificationNodes);
        //AudioManager.Instance.PlayNotificationPopUp();
    }

    private List<NotificationNode> GetRandomNotificationNodeList()
    {
        int randomId = Random.Range(1, notificationsBST.Count);
        List<NotificationNode> notificationNodes = new List<NotificationNode>(GetNotificationList(randomId));

        return notificationNodes;
    }

    private List<NotificationNode> GetNotificationList(int id)
    {
        return notificationsBST.Search(id);
    }
}
