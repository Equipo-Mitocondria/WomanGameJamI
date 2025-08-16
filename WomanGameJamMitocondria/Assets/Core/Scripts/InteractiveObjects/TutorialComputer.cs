using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialComputer : Computer
{
    private bool _isFirstTimeInteract = true;

    [SerializeField] private GameObject _tutorialCat;
    [SerializeField] private GameObject _tutorialInteractionPrompt;
    [SerializeField] private TutorialDialogueManager _tutorialDialogueManager;

    [SerializeField] private MeshRenderer[] _meshesGlowing;

    public override void Interact()
    {
        base.Interact();

        if (!_isFirstTimeInteract)
            return;
        
        _isFirstTimeInteract = false;
        RemoveGlowingMaterial();
        StartCoroutine(HideTutorialInteractionPromptOnStopWorking());

        _work.CanGetUp = false;

        _tutorialCat.SetActive(true);
        _tutorialDialogueManager.PrepareToShowTutorialInteractionPrompt();
        NotificationsManager.Instance.TutorialNotificationSpawn();
    }

    public void ShowTutorialInteractionPrompt()
    {
        _tutorialInteractionPrompt.SetActive(true);
        _work.CanGetUp = true;
    }

    IEnumerator HideTutorialInteractionPromptOnStopWorking()
    {
        yield return new WaitUntil(() => !_work.IsWorking);
        HideTutorialInteractionPrompt();
    }

    public void HideTutorialInteractionPrompt()
    {
        _tutorialInteractionPrompt.SetActive(false);
    }

    private void RemoveGlowingMaterial()
    {
        foreach (var mesh in _meshesGlowing)
        {
            Material[] newMaterials = new Material[mesh.materials.Length - 1];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = mesh.materials[i];
            }

            mesh.materials = newMaterials;
        }
    }
}
