using System.Collections;
using UnityEngine;

public class TutorialUIManager : UIManager
{
    [Header("Tutorial")]

    [SerializeField] private float _timeSanityGoDown;
    [SerializeField] private SanityEffect _tutorialSanityEffect;

    protected override IEnumerator StopNotificationThread()
    {
        yield return new WaitForSeconds(_timeBeforeClearNotifications);

        ClearNotifications();

        Sanity.Instance.ApplySanityEffect(_tutorialSanityEffect);
        StartCoroutine(WaitForSanityToGoDown());
    }

    private IEnumerator WaitForSanityToGoDown()
    {
        yield return new WaitForSeconds(_timeSanityGoDown);

        DialogueManager.Instance.TriggerDialogue(13);
    }
}
