using System.Collections.Generic;
using UnityEngine;

public class NotificationsBST : MonoBehaviour
{
    private NotificationBSTNode _root;

    public NotificationsBST(List<NotificationList> notificationListsList)
    {
        InsertListBalanced(notificationListsList);
    }

    private void InsertListBalanced(List<NotificationList> notificationListsList)
    {
        _root = new NotificationBSTNode(notificationListsList, 0, notificationListsList.Count - 1);
    }

    public List<NotificationNode> Search(int id)
    {
        if (_root == null)
            throw new System.Exception("DialogueBST is empty and cannot find the item.");
        else
        {
            return _root.Search(id);
        }
    }

    private class NotificationBSTNode
    {
        private List<NotificationNode> _notificationNodeList;
        private NotificationBSTNode _left;
        private NotificationBSTNode _right;
        private int _id;

        public NotificationBSTNode(List<NotificationList> notificationListsList, int start, int end)
        {
            if (start > end)
                return;

            int midList = (start + end) / 2;
            NotificationList notificationList = notificationListsList[midList];
            _notificationNodeList = notificationList.List;
            _id = notificationList.ID;

            _left = new NotificationBSTNode(notificationListsList, start, midList - 1);
            _right = new NotificationBSTNode(notificationListsList, midList + 1, end);
        }

        public List<NotificationNode> Search(int id)
        {
            if (id == _id)
            {
                return _notificationNodeList;
            }
            else
            {
                if (id < _id)
                {
                    if (_left != null)
                    {
                        return _left.Search(id);
                    }
                    else
                    {
                        throw new System.Exception($"There are not dialogue lines for id {id}.");
                    }
                }
                else
                {
                    if (_right != null)
                    {
                        return _right.Search(id);
                    }
                    else
                    {
                        throw new System.Exception($"There are not dialogue lines for id {id}.");
                    }
                }
            }
        }
    }
}
