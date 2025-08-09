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
    [SerializeField] private int _maxNumberNotification;
    [SerializeField] private float _timeBetweenNotifications;
    [SerializeField] private float _timeBeforeClearNotifications;

    private List<NotificationHUDPanel> _notificationHUDList;
    private Coroutine _activeNotificationThread;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartNotificationThread(List<NotificationNode> notificationNodes)
    {
        if (IsThereAnActiveThread())
            ClearNotifications();

        _activeNotificationThread = StartCoroutine(ShowNextNotificationInThread(notificationNodes));
    }

    private IEnumerator ShowNextNotificationInThread(List<NotificationNode> notificationNodes)
    {
        if (IsThreadEmpty(notificationNodes))
        {
            StartCoroutine(StopNotificationThread());

            if(IsThereAnActiveThread())
                StopCoroutine(_activeNotificationThread);
        }

        NotificationNode nextNotification = notificationNodes.FirstOrDefault();
        notificationNodes.Remove(nextNotification);

        AddNotificationToHUD(nextNotification);

        yield return new WaitForSeconds(_timeBetweenNotifications);

        _activeNotificationThread = StartCoroutine(ShowNextNotificationInThread(notificationNodes));
    }

    private void AddNotificationToHUD(NotificationNode nextNotification)
    {
        if (IsHUDNotificationsFull())
            RemoveFirstNotificationFromHUD();

        NotificationHUDPanel notificationHUD;

        if (nextNotification.speaker == NotificationNode.Speakers.Yuko)
            notificationHUD = Instantiate(_leftMessagePrefab, _hud.gameObject.transform).GetComponent<NotificationHUDPanel>();
        else
            notificationHUD = Instantiate(_rightMessagePrefab, _hud.gameObject.transform).GetComponent<NotificationHUDPanel>();

        notificationHUD.SetNotificationVisuals(nextNotification.avatar, nextNotification.message);
        _notificationHUDList.Add(notificationHUD);
    }

    private IEnumerator StopNotificationThread()
    {
        yield return new WaitForSeconds(_timeBeforeClearNotifications);

        ClearNotifications();
    }

    private bool IsHUDNotificationsFull()
    {
        if(_notificationHUDList.Count >= _maxNumberNotification)
            return true;
        else
            return false;
    }

    private bool IsThereAnActiveThread()
    {
        return _activeNotificationThread != null;
    }

    private bool IsThreadEmpty(List<NotificationNode> notificationNodes)
    {
        return notificationNodes.Count <= 0;
    }

    private void RemoveFirstNotificationFromHUD()
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

        if(IsThereAnActiveThread())
            StopCoroutine(_activeNotificationThread);

        _activeNotificationThread = null;
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
