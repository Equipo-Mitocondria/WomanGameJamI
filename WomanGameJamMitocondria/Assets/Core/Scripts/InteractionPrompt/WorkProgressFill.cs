using UnityEngine;
using UnityEngine.UI;

public class WorkProgressFill : MonoBehaviour
{
    private Image _progressBar;

    public void UpdateProgressBar(float newProgress)
    {
        if (_progressBar == null)
            _progressBar = this.GetComponent<Image>();

        _progressBar.fillAmount = newProgress;
    }
}
