using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreName;
    public TextMeshProUGUI highScore;
    public TMP_Dropdown typeSelection;
    public Slider volumeSlider;
    private void Start()
    {
        GameManager.Instance.cursor.EnterPauseMenu();
        GetHighScoreName();
        GetHighScore();
        AudioManager.Instance.setVolume(PlayerPrefs.GetFloat("vol"));
        SetVolumeSlider(PlayerPrefs.GetFloat("vol"));
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

    public void SetVolume()
    {
        AudioManager.Instance.setVolume(volumeSlider.value);
    }
    public void SetVolumeSlider(float vol)
    {
        volumeSlider.value = vol;
    }
}
