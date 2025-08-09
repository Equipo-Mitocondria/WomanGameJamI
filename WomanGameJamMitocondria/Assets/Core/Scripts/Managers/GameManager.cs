using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float _time;

    public float Time {  get { return _time; } }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void EndTask()
    {
        //TODO
    }

    public void GameOver()
    {
        //TODO
    }
}
