using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static SceneManager.Scenes CurrentPhase;

    private float _time;

    public float Time {  get { return _time; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            CurrentPhase = SceneManager.Scenes.Phase1;
        }
        else
            Destroy(gameObject);
    }

    public void EndTask()
    {
        switch (CurrentPhase)
        {
            case SceneManager.Scenes.Phase1:
                CurrentPhase = SceneManager.Scenes.Phase2;
                SceneManager.Instance.LoadScene(CurrentPhase);
                break;
            case SceneManager.Scenes.Phase2:
                CurrentPhase = SceneManager.Scenes.Phase3;
                SceneManager.Instance.LoadScene(CurrentPhase);
                break;
            case SceneManager.Scenes.Phase3:
                EndPlay();
                break;
        }
    }

    public void BeginPlay()
    {
        CurrentPhase = SceneManager.Scenes.Phase1;
        SceneManager.Instance.LoadScene(CurrentPhase);
    }
    public void EndPlay()
    {
        CurrentPhase = SceneManager.Scenes.TitleScreen;
        SceneManager.Instance.LoadScene(CurrentPhase);
    }

    public void GameOver()
    {
        EndPlay();
    }

}
