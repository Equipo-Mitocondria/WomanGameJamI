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
}
