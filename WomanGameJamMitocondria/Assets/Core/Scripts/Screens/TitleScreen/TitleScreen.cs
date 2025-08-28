using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _volumeButton;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _howToPlayBoxA;
    [SerializeField] private GameObject _howToPlayBoxB;

    private Color _originalTextColor;

    private InputActions _inputActions;

    private void Start()
    {
        _originalTextColor = _startButton.GetComponentInChildren<TMP_Text>().color;
        _inputActions = new InputActions();

        _howToPlayBoxA.SetActive(false);
        _howToPlayBoxA.SetActive(false);
    }

    public void PlayStartButton()
    {
        AudioManager.Instance.PlayUIClick();
        GameManager.Instance.BeginTutorial();
    }
    
    public void PlayVolumeButton()
    {
        AudioManager.Instance.PlayUIClick();
        // TO DO
    }

    public void PlayHowToPlayButton()
    {
        AudioManager.Instance.PlayUIClick();
        _howToPlayBoxA.SetActive(true);
        _inputActions.UI.TriggerHowToPlay.performed += TriggerNextHowToPlayPanel;
        _inputActions.UI.Enable();
    }

    private void TriggerNextHowToPlayPanel(InputAction.CallbackContext context)
    {
        _howToPlayBoxA.SetActive(false);
        _howToPlayBoxB.SetActive(true);
        _inputActions.UI.TriggerHowToPlay.performed -= TriggerNextHowToPlayPanel;
        _inputActions.UI.TriggerHowToPlay.performed += CloseHowToPlayPanel;
    }

    private void CloseHowToPlayPanel(InputAction.CallbackContext context)
    {
        _howToPlayBoxB.SetActive(false);
        _inputActions.UI.TriggerHowToPlay.performed -= CloseHowToPlayPanel;
        _inputActions.UI.Disable();
    }

    public void PlayExitButton()
    {
        AudioManager.Instance.PlayUIClick();
        Application.Quit();
    }

    public void HoverPlayButton()
    {
        AudioManager.Instance.PlayUIHover();
        _startButton.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

    public void HoverVolumeButton()
    {
        AudioManager.Instance.PlayUIHover();
        _volumeButton.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

    public void HoverHowToPlayButton()
    {
        AudioManager.Instance.PlayUIHover();
        _howToPlayButton.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

    public void HoverExitButton()
    {
        AudioManager.Instance.PlayUIHover();
        _exitButton.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

    public void StopHoverPlayButton()
    {
        _startButton.GetComponentInChildren<TMP_Text>().color = _originalTextColor;
    }

    public void StopHoverVolumeButton()
    {
        _volumeButton.GetComponentInChildren<TMP_Text>().color = _originalTextColor;
    }

    public void StopHoverHowToPlayButton()
    {
        _howToPlayButton.GetComponentInChildren<TMP_Text>().color = _originalTextColor;
    }

    public void StopHoverExitButton()
    {
        _exitButton.GetComponentInChildren<TMP_Text>().color = _originalTextColor;
    }

    private void OnDisable()
    {
        _inputActions = new InputActions();
    }

    private void OnEnable()
    {
        _inputActions = new InputActions();
    }
}
