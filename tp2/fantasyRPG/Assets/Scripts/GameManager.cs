using UnityEngine;
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
        AudioManager.Instance.setVolume(PlayerPrefs.GetFloat("vol"));

    }

    public void StartGame(int typeSelected)
    {
        _typeIndexSelected = typeSelected;
        cursor.EnterGameMode();
        SceneManager.LoadScene("Scenes/Dungeon");
    }

    public int GetTypeChosen()
    {
        return _typeIndexSelected;
    }
}
