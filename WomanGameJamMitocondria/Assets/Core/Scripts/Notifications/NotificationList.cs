using System.Collections.Generic;
using UnityEngine;

public class NotificationList : MonoBehaviour
{
    private List<NotificationNode> _notificationList;
    private int _level;
    private int _id;

    public List<NotificationNode> List { get { return _notificationList; } private set { _notificationList = value; } }
    public int Level { get { return _level; } }
    public int ID { get { return _id; } }

    public NotificationList(int level, int id)
    {
        _notificationList = new List<NotificationNode>();
        _level = level;
        _id = id;
    }

    public void Add(NotificationNode notification)
    {
        _notificationList.Add(notification);
    }
}
