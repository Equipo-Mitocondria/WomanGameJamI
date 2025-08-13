using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _volumeButton;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _howToPlayBox;

    public void PlayStartButton()
    {
        AudioManager.Instance.PlayUIClick();
        GameManager.Instance.BeginPlay();
    }
    
    public void PlayVolumeButton()
    {
        AudioManager.Instance.PlayUIClick();
        // TO DO
    }

    public void PlayHowToPlayButton()
    {
        AudioManager.Instance.PlayUIClick();
        // TO DO
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
        _startButton.GetComponentInChildren<TMP_Text>().color = Color.white;
    }

    public void StopHoverVolumeButton()
    {
        _volumeButton.GetComponentInChildren<TMP_Text>().color = Color.white;
    }

    public void StopHoverHowToPlayButton()
    {
        _howToPlayButton.GetComponentInChildren<TMP_Text>().color = Color.white;
    }

    public void StopHoverExitButton()
    {
        _exitButton.GetComponentInChildren<TMP_Text>().color = Color.white;
    }
}
