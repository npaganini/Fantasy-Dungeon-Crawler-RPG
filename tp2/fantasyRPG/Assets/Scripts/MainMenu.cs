﻿using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    public TMP_Dropdown typeSelection;
    public Slider volumeSlider;

    private void Start()
    {
        
        GameManager.Instance.cursor.EnterPauseMenu();
        GetHighScore();
        AudioManager.Instance.setVolume(PlayerPrefs.GetFloat("vol", 100f));
        SetVolumeSlider(PlayerPrefs.GetFloat("vol", 100f));
    }

    public void BeginGame()
    {
        GameManager.Instance.StartGame(typeSelection.value);
    }

    private void GetHighScore()
    {
        // get high score from persistant memory
        var bestTime = PlayerPrefs.GetFloat("High Score", -1f);
        if(bestTime != -1){
            bestTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(bestTime / 60F);
            int seconds = Mathf.FloorToInt(bestTime - minutes * 60);
            highScore.SetText(new StringBuilder(minutes.ToString("00") + ":" + seconds.ToString("00")));
        }
        else
        {
            highScore.SetText("None!");
        }
        
        
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
