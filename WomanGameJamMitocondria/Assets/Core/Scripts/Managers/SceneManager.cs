using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public enum Scenes { TitleScreen = 0, Phase1 = 1, Phase2 = 2, Phase3 = 3 , Credits = 4, Tutorial = 5 }

    public static SceneManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void LoadScene(int sceneToLoad)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
