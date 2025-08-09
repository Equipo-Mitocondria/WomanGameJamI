using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private int _currentLevel;
    private DialogueBST _dialogueBST;
    private bool _doNextDialogue = false;
    private bool _isInDialogue = false;
    public bool DoNextDialogue { get { return _doNextDialogue; } set {  _doNextDialogue = value; } }
    public bool IsInDialogue { get { return _isInDialogue; } }

    private void Awake()
    {
        instance = this;
        string csv = "";
        //string csv = CSVImporter.ImportCSV(path);
        List<string[]> parsedCSV = CSVParser.ParseCSV(csv);
        _dialogueBST = new DialogueBST(DialogueBuilder.BuildDialogueListsList(parsedCSV, _currentLevel));
    }

    public void TriggerDialogue(int id)
    {
        _isInDialogue = true;
        List<DialogueNode> dialogueNodes = GetDialogueNodeList(id);
        StartCoroutine(ShowDialogues(dialogueNodes));
    }

    private List<DialogueNode> GetDialogueNodeList(int id)
    {
        return _dialogueBST.Search(id);
    }

    IEnumerator ShowDialogues(List<DialogueNode> dialogueNodes)
    {
        //UIManager.instance.ShowDialoguePanel();
        foreach (DialogueNode node in dialogueNodes)
        {            
            string character = node.Character;
            string text = node.Text;
                
            //UIManager.instance.UpdateDialoguePanel(character, text);
                
            while(!_doNextDialogue)
                yield return null;

            _doNextDialogue = false;
        }
        //UIManager.instance.HideDialoguePanel();
        _isInDialogue = false;
        //EventHolder.instance.onEndDialogue?.Invoke();
    }
}
