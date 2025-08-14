using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private float _timeUntilPlaceButtons;

    private Color _color;

    private void Start()
    {
        _color = _continueButton.GetComponentInChildren<TMP_Text>().color;
        _continueButton.gameObject.SetActive(false);
        _quitButton.gameObject.SetActive(false);

        StartCoroutine(PlaceButtonsOnScreen());
    }

    private IEnumerator PlaceButtonsOnScreen()
    {
        yield return new WaitForSeconds(_timeUntilPlaceButtons);

        _continueButton.gameObject.SetActive(true);
        _quitButton.gameObject.SetActive(true);
    }

    public void PlayContinueButton()
    {
        AudioManager.Instance.PlayUIClick();
        GameManager.Instance.Continue();
    }

    public void PlayQuitButton()
    {
        AudioManager.Instance.PlayUIClick();
        GameManager.Instance.GameOver();
    }

    public void HoverContinueButton()
    {
        AudioManager.Instance.PlayUIHover();
        _continueButton.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

    public void HoverQuitButton()
    {
        AudioManager.Instance.PlayUIHover();
        _quitButton.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

    public void StopHoverContinueButton()
    {
        _continueButton.GetComponentInChildren<TMP_Text>().color = _color;
    }

    public void StopHoverQuitButton()
    {
        _quitButton.GetComponentInChildren<TMP_Text>().color = _color;
    }
}
