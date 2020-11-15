using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // todo: add audio
    // public AudioMixer audioMixer;
    // public Slider volumeslider;
    private SetCursor _cursor;
    public GameObject pauseMenuUI;
    public GameObject player;
    private bool isGamePaused = false;

    // public void OnEnable(){
    //     volumeslider = FindObjectOfType<Slider>(); todo: add audio
    //     volumeslider.value =  FindObjectOfType<AudioManager>().getVolume(); todo: add audio
    // }

    public void Start()
    {
        Resume();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        // Cursor.visible = false;
        Debug.Log("Resumeee");
        pauseMenuUI.SetActive(false);
        player.GetComponent<PlayerLogic>().enabled = true;
        GameManager.Instance.cursor.EnterGameMode();
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void SetVolume(float volume)
    {
        // FindObjectOfType<AudioManager>().changeVolume(volume); todo: add audio
        //audioMixer.SetFloat("volume", volume);
    }
    
    
    public void ExitGame()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}