using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Playables;
using static NotificationNode;

public class NotificationsBuilder
{
    // BuildNotificationNodesList converts all csv raw data into NotificationNodes elements
    // BuildNotificationListsList stores each 'chunck' of dialogue on separate NotificationList elements
    public static List<NotificationList> BuildNotificationListsList(List<string[]> parsedCSV, int currentLevel)
    {
        List<NotificationNode> notificationNodesList = BuildNotificationNodesList(parsedCSV);
        List<NotificationList> notificationListsList = BuildNotificationListsList(currentLevel, notificationNodesList);

        return notificationListsList;
    }

    private static List<NotificationNode> BuildNotificationNodesList(List<string[]> parsedCSV)
    {
        List<NotificationNode> notificationNodesList = new List<NotificationNode>();
        foreach (string[] row in parsedCSV)
        {
            //Change based on content of NotificationNode
            //NotificationNode node = new NotificationNode(Convert.ToInt32(row[0]), Convert.ToInt32(row[1]), row[2], row[3].Remove(row[3].Length - 1));
            NotificationNode node = new NotificationNode(Convert.ToInt32(row[0]), Convert.ToInt32(row[1]), row[2], row[3], row[4], row[5], float.Parse(row[6]));
            notificationNodesList.Add(node);
        }
        return notificationNodesList;
    }

    private static List<NotificationList> BuildNotificationListsList(int currentLevel, List<NotificationNode> notificationNodesList)
    {
        List<NotificationList> notificationListsList = new List<NotificationList>();

        int currentID = 1;
        NotificationList currentNotificationList = new NotificationList(currentLevel, currentID);
        notificationListsList.Add(currentNotificationList);

        // For each DialogueNode (Notification) checks its level
        // If the level is the one we're trying to load checks the ID
        // If the ID equals currentID adds the node to the list
        // If not, creates a new DialogueList
        //
        // The idea is only to load the lists of the current level and stop building
        // once we have finished loading the level (csv must be in order for this to
        // work properly)

        foreach (NotificationNode node in notificationNodesList)
        {
            if (currentLevel == node.level)
            {
                if (currentNotificationList.ID != node.id)
                {
                    currentID++;
                    currentNotificationList = new NotificationList(currentLevel, currentID);
                    notificationListsList.Add(currentNotificationList);
                }
                currentNotificationList.Add(node);
            }
            else if (currentLevel < node.level)
            {
                break;
            }
        }

        return notificationListsList;
    }
}
