using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationHUDPanel : MonoBehaviour
{
    [SerializeField] private Image _avatarHUD;
    [SerializeField] private TMP_Text _messageHUD;

    public void SetNotificationVisuals(Image avatar, string message)
    {
        _avatarHUD = avatar;
        _messageHUD.text = message;
    }
}
