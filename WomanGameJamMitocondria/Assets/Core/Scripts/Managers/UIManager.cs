using TMPro;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Dialogue")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueTMPro;
    [SerializeField] private GameObject _leftMessagePrefab, _rightMessagePrefab;
    [SerializeField] private Canvas _hud;
    [SerializeField] int _maxNumberNotification;

    private List<NotificationHUDPanel> _notificationHUDList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private IEnumerator ShowNotificationPopUp(string message, string speakerName, Image avatar, bool isLeft = true)
    {
        if (AreNotificationsFull()){
            RemoveFirstNotification();
        }

        NotificationHUDPanel notificationHUD;

        if (isLeft)
            notificationHUD = Instantiate(_leftMessagePrefab, _hud.gameObject.transform).GetComponent<NotificationHUDPanel>();
        else
            notificationHUD = Instantiate(_rightMessagePrefab, _hud.gameObject.transform).GetComponent<NotificationHUDPanel>();

        notificationHUD.SetNotificationVisuals(avatar, message);
        _notificationHUDList.Add(notificationHUD);

        yield return null;
    }

    private bool AreNotificationsFull()
    {
        if(_notificationHUDList.Count >= _maxNumberNotification)
            return true;
        else
            return false;
    }

    private void RemoveFirstNotification()
    {
        NotificationHUDPanel first = _notificationHUDList.First();
        Destroy(first.gameObject);
        _notificationHUDList.Remove(first);
    }

    private void ClearNotifications()
    {
        foreach(var element in _notificationHUDList)
        {
            Destroy(element.gameObject);
        }

        _notificationHUDList.Clear();
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
