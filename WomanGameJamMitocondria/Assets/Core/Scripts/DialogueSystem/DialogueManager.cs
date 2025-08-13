using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private int _currentLevel;
    [SerializeField] private int _timeBetweenDialogues;
    private DialogueBST _dialogueBST;
    private bool _doNextDialogue = false;
    private bool _isInDialogue = false;
    private Coroutine _showDialogueCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);        
    }

    private void Start()
    {
        string csv = CSVImporter.ImportCSV(Application.streamingAssetsPath + "/DialoguesSimplified.csv");
        List<string[]> parsedCSV = CSVParser.ParseCSV(csv);
        _dialogueBST = new DialogueBST(DialogueBuilder.BuildDialogueListsList(parsedCSV, _currentLevel));
    }

    public void TriggerDialogue(int id)
    {
        if(_isInDialogue && _showDialogueCoroutine != null)
            StopCoroutine(_showDialogueCoroutine);
            
        _isInDialogue = true;
        List<DialogueNode> dialogueNodes = GetDialogueNodeList(id);
        _showDialogueCoroutine = StartCoroutine(ShowDialogues(dialogueNodes));
    }

    private List<DialogueNode> GetDialogueNodeList(int id)
    {
        return _dialogueBST.Search(id);
    }

    IEnumerator ShowDialogues(List<DialogueNode> dialogueNodes)
    {
        UIManager.Instance.ShowDialoguePanel();
        foreach (DialogueNode node in dialogueNodes)
        {            
            string character = node.Character;
            string text = node.Text;
                
            UIManager.Instance.UpdateDialoguePanel(text);
                
            yield return new WaitForSeconds(_timeBetweenDialogues);
        }
        UIManager.Instance.HideDialoguePanel();
        _isInDialogue = false;
        //EventHolder.instance.onEndDialogue?.Invoke();
    }
}
