using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private Button _continueButton;

    private Color _color;

    private void Start()
    {
        _color = _continueButton.GetComponentInChildren<TMP_Text>().color;
    }

    public void PlayContinueButton()
    {
        AudioManager.Instance.PlayUIClick();
        GameManager.Instance.NextPhase();
    }

    public void HoverContinueButton()
    {
        AudioManager.Instance.PlayUIHover();
        _continueButton.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

    public void StopHoverContinueButton()
    {
        _continueButton.GetComponentInChildren<TMP_Text>().color = _color;
    }
}
