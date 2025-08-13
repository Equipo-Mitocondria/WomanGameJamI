using UnityEngine;

public class TutorialComputer : Computer
{
    private bool _isFirstTimeInteract = true;

    [SerializeField] private GameObject _tutorialCat;
    [SerializeField] private GameObject _tutorialInteractionPrompt;
    [SerializeField] private TutorialDialogueManager _tutorialDialogueManager;

    public override void Interact()
    {
        base.Interact();

        if (_isFirstTimeInteract)
        {
            _isFirstTimeInteract = false;
            NotificationsManager.Instance.TutorialNotificationSpawn();
            _tutorialDialogueManager.PrepareToShowTutorialInteractionPrompt();
            _tutorialCat.SetActive(true);
        }
        else
        {
            HideTutorialInteractionPrompt();
        }
    }

    public void ShowTutorialInteractionPrompt()
    {
        _tutorialInteractionPrompt.SetActive(true);
    }

    public void HideTutorialInteractionPrompt()
    {
        _tutorialInteractionPrompt.SetActive(false);
    }
}
