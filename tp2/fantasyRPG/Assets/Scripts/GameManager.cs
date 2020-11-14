using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviorSingleton<GameManager>
{
    public GameObject gameManager;
    public TMP_Dropdown typeDropdown;
    private int _typeIndexSelected;

    void Start()
    {
        DontDestroyOnLoad(gameManager);
    }

    public void StartGame()
    {
        _typeIndexSelected = GetSelectedOption();
        Debug.Log("Selected: " + _typeIndexSelected);
        SceneManager.LoadScene("Scenes/Demo");
    }

    private int GetSelectedOption()
    {
        return typeDropdown.value;
    }

}
