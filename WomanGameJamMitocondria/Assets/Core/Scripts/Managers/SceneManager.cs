using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
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
