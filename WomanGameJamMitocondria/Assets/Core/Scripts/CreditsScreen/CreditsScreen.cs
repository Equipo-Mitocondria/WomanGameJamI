using System.Collections;
using UnityEngine;

public class CreditsScreen : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GameManager.Instance.FinishCredits());
    }

    
}
