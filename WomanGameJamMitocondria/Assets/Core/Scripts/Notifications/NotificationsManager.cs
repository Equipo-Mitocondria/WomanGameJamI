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
    [SerializeField] bool _spawnIntroductionNotification;
    [SerializeField] bool _startNotificationRoutine;

    NotificationsBST notificationsBST;

    private Coroutine _notificationCoroutine;

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
        notificationsBST = new NotificationsBST(NotificationsBuilder.BuildNotificationListsList(parsedCSV, GameManager.Instance.CurrentPhase));

        if(_spawnIntroductionNotification)
            NotificationSpawnWithID(0);

        if (_startNotificationRoutine)
            StartCoroutine(NotificationPeriod());
    }

    public void TutorialNotificationSpawn()
    {
        NotificationSpawnWithID(0);
    }

    private IEnumerator NotificationPeriod()
    {
        yield return new WaitForSeconds(_notificationPeriod);

        if (WillNotificationSpawn())
            NotificationSpawn();

        _notificationCoroutine = StartCoroutine(NotificationPeriod());
    }

    private bool WillNotificationSpawn()
    {
        if(Random.Range(0f, 1f) <= _notificationChance)
            return true;
        else
            return false;
    }

    public void NotificationSpawnWithID(int nodeId)
    {
        List<NotificationNode> notificationNodes = GetNotificationList(nodeId);

        NotificationNode first = notificationNodes.FirstOrDefault();
        Sanity.Instance.ApplySanityEffect(first.sanityEffect);

        UIManager.Instance.StartNotificationThread(notificationNodes);
        //AudioManager.Instance.PlayNotificationPopUp();
    }
    
    private void NotificationSpawn()
    {
        List<NotificationNode> notificationNodes = GetRandomNotificationNodeList();

        NotificationNode first = notificationNodes.FirstOrDefault();
        Sanity.Instance.ApplySanityEffect(first.sanityEffect);

        UIManager.Instance.StartNotificationThread(notificationNodes);
        //AudioManager.Instance.PlayNotificationPopUp();
    }

    public void StopNotificationCoroutines()
    {
        if (_notificationCoroutine != null)
        {
            StopCoroutine(_notificationCoroutine);
            _notificationCoroutine = null;
        }

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
