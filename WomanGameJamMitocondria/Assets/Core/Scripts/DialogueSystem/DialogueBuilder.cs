using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DialogueBuilder
{
    // BuildDialogueNodesList converts all csv raw data into DialogueNodes (Notification) elements
    // BuildDialogueListsList stores each 'chunck' of dialogue on separate DialogueList elements (I've implemented NotificationList)
    public static List<DialogueList> BuildDialogueListsList(List<string[]> parsedCSV, int currentLevel)
    {        
        List<DialogueNode> dialogueNodesList = BuildDialogueNodesList(parsedCSV); //Notification
        List<DialogueList> dialogueListsList = BuildDialogueListsList(currentLevel, dialogueNodesList); //NotificationList

        return dialogueListsList;
    }

    private static List<DialogueNode> BuildDialogueNodesList(List<string[]> parsedCSV)
    {
        List<DialogueNode> dialogueNodesList = new List<DialogueNode>();
        foreach (string[] row in parsedCSV)
        {
            //Change based on content of Notification
            DialogueNode node = new DialogueNode(Convert.ToInt32(row[0]), Convert.ToInt32(row[1]), row[2], row[3].Remove(row[3].Length - 1));
            dialogueNodesList.Add(node);
        }
        return dialogueNodesList;
    }

    private static List<DialogueList> BuildDialogueListsList(int currentLevel, List<DialogueNode> dialogueNodesList) 
    {
        List<DialogueList> dialogueListsList = new List<DialogueList>();

        int currentID = 1;
        DialogueList currentDialogueList = new DialogueList(currentLevel, currentID);
        dialogueListsList.Add(currentDialogueList);

        // For each DialogueNode (Notification) checks its level
        // If the level is the one we're trying to load checks the ID
        // If the ID equals currentID adds the node to the list
        // If not, creates a new DialogueList
        //
        // The idea is only to load the lists of the current level and stop building
        // once we have finished loading the level (csv must be in order for this to
        // work properly)

        foreach (DialogueNode node in dialogueNodesList)
        {
            if(currentLevel == node.Level)
            {
                if(currentDialogueList.ID != node.ID)
                {
                    currentID++;
                    currentDialogueList = new DialogueList(currentLevel, currentID);
                    dialogueListsList.Add(currentDialogueList);
                }
                currentDialogueList.Add(node);
            }
            else if (currentLevel < node.Level)
            {
                break;
            }
        }

        return dialogueListsList;
    }
}
