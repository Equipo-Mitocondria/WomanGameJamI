using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _volumeButton;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _exitButton;

    public void PlayStartButton()
    {
        GameManager.Instance.BeginPlay();
    }
    
    public void PlayVolumeButton()
    {
        // TO DO
    }

    public void PlayHowToPlayButton()
    {
        // TO DO
    }
    
    public void PlayExitButton()
    {
        Application.Quit();
    }

    public void HoverPlayButton()
    {
        _startButton.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

    public void HoverVolumeButton()
    {
       _volumeButton.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

    public void HoverHowToPlayButton()
    {
        _howToPlayButton.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

    public void HoverExitButton()
    {
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
