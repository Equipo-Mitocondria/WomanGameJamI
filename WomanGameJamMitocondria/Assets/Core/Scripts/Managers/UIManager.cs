using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Dialogue")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueTMPro;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowNotificationPopUp(string message, string speakerName, Image avatar)
    {
        //TO DO
    }

    public void ShowDialoguePanel()
    {
        _dialoguePanel.SetActive(true);
    }

    public void UpdateDialoguePanel(string text)
    {
        _dialogueTMPro.text = text;
    }

    public void HideDialoguePanel()
    {
        _dialoguePanel.SetActive(false);
    }
}
