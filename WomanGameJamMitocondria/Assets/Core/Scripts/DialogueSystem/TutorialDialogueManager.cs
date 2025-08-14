using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialogueManager : DialogueManager
{
    [SerializeField] private TutorialComputer _tutorialComputer;

    private bool _doShowTutorialInteractionPrompt = false;
    private bool _doBeginningEndTutorial = false;
    private bool _doEndTutorial = false;

    protected override IEnumerator ShowDialogues(List<DialogueNode> dialogueNodes)
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

        if (_doShowTutorialInteractionPrompt)
        {
            _doShowTutorialInteractionPrompt = false;
            _doBeginningEndTutorial = true;
            _tutorialComputer.ShowTutorialInteractionPrompt();
        } else if (_doBeginningEndTutorial)
        {
            _doBeginningEndTutorial = false;
            _doEndTutorial = true;
            DialogueManager.Instance.TriggerDialogue(14);
        } else if (_doEndTutorial)
        {
            _doEndTutorial = false;
            GameManager.Instance.NextPhase();
        }
    }

    public void PrepareToShowTutorialInteractionPrompt()
    {
        _doShowTutorialInteractionPrompt = true;
    }
}
