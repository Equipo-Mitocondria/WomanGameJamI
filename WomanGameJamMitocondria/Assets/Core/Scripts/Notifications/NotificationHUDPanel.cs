using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationHUDPanel : MonoBehaviour
{
    [SerializeField] private Image _avatarHUD;
    [SerializeField] private TMP_Text _messageHUD;

    public void SetNotificationVisuals(Sprite avatar, string message)
    {
        _avatarHUD.sprite = avatar;
        _messageHUD.text = message;
    }
}
