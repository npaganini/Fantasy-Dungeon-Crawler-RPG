using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PauseMenu : MonoBehaviour
{
    // todo: add audio
    // public AudioMixer audioMixer;
    // public Slider volumeslider;
    private SetCursor _cursor;
    public GameObject pauseMenuUI;
    public GameObject player;
    private bool isGamePaused = false;
    public Slider volumeSlider;
    public GameObject PauseFirstButton;
    public GameObject OptionsFirstButton;
    public GameObject LastSelected;

    // public void OnEnable(){
    //     volumeslider = FindObjectOfType<Slider>(); todo: add audio
    //     volumeslider.value =  FindObjectOfType<AudioManager>().getVolume(); todo: add audio
    // }

    public void Start()
    {
        SetVolumeSlider(PlayerPrefs.GetFloat("vol"));
        Resume();
    }
    public void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //set selected button
        EventSystem.current.SetSelectedGameObject(PauseFirstButton);
        LastSelected = PauseFirstButton;
    }
    public void OptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(OptionsFirstButton);
        LastSelected = OptionsFirstButton;
    }
    public void BackToMenu()
    {
        EventSystem.current.SetSelectedGameObject(PauseFirstButton);
        LastSelected = PauseFirstButton;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Menu"))
        {
            if (isGamePaused)
            {

                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        player.GetComponent<PlayerLogic>().enabled = false;
        GameManager.Instance.cursor.EnterPauseMenu();
        pauseMenuUI.SetActive(true);
        // Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        player.GetComponent<PlayerLogic>().enabled = true;
        GameManager.Instance.cursor.EnterGameMode();
        isGamePaused = false;
        Time.timeScale = 1f;
    }
    
    public void ExitGame()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
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