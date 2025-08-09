using UnityEngine;

public class Fireplace : SanityObject
{
    [SerializeField] private int _dialogueID;

    public override void Interact()
    {
        base.Interact();

        DialogueManager.Instance.TriggerDialogue(_dialogueID);
    }
}
