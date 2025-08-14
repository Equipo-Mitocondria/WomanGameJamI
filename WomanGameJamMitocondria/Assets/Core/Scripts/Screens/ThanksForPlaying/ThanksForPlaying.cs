using UnityEngine;

public class ThanksForPlaying : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GameManager.Instance.FinishThanksForPlaying());
    }
}
