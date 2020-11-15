using System;
using System.Text;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreName;
    public TextMeshProUGUI highScore;
    public TMP_Dropdown typeSelection;

    private void Start()
    {
        GetHighScoreName();
        GetHighScore();
    }

    public void BeginGame()
    {
        GameManager.Instance.StartGame(typeSelection.value);
    }

    private void GetHighScoreName()
    {
        highScoreName.SetText(PlayerPrefs.GetString("High Score Name", "Be the first to play!"));
    }

    private void GetHighScore()
    {
        // get high score from persistant memory
        var bestTime = PlayerPrefs.GetFloat("High Score", 0);
        bestTime += Time.deltaTime;
        // OnGUI();
        int minutes = Mathf.FloorToInt(bestTime / 60F);
        int seconds = Mathf.FloorToInt(bestTime - minutes * 60);
        highScore.SetText(new StringBuilder(minutes.ToString("00")  + ":" + seconds.ToString("00")));
    }
}
