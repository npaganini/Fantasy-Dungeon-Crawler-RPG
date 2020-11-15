using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviorSingleton<GameManager>
{
    private int _typeIndexSelected;
    public SetCursor cursor;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(cursor);
        cursor.EnterPauseMenu();
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void StartGame(int typeSelected)
    {
        _typeIndexSelected = typeSelected;
        cursor.EnterGameMode();
        SceneManager.LoadScene("Scenes/Demo");
    }

    public int GetTypeChosen()
    {
        return _typeIndexSelected;
    }
}
