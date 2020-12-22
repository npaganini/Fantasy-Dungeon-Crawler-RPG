using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    public TMP_Dropdown typeSelection;
    public Slider volumeSlider;
    public GameObject MenuFirstButton;
    public GameObject OptionsFirstButton;
    public GameObject OptionsClosedButton;
    public GameObject ControlOptionsButton;
    public GameObject PlayFirstButton;
    public GameObject PickTypeFirstButton;
    public GameObject LastSelected;
    private void Start()
    {
        GameManager.Instance.cursor.EnterPauseMenu();
        GetHighScore();
        AudioManager.Instance.setVolume(PlayerPrefs.GetFloat("vol", 100f));
        SetVolumeSlider(PlayerPrefs.GetFloat("vol", 100f));
    }

    public void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(LastSelected);
    }

    public void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //set selected button
        LastSelected = MenuFirstButton;
        EventSystem.current.SetSelectedGameObject(MenuFirstButton);
    }

    public void BeginGame()
    {
        GameManager.Instance.StartGame(typeSelection.value);
    }

    public void OpenOptions()
    {
        EventSystem.current.SetSelectedGameObject(OptionsFirstButton);
        LastSelected = OptionsFirstButton;
    }

    public void OpenControls()
    {
        EventSystem.current.SetSelectedGameObject(ControlOptionsButton);
        LastSelected = ControlOptionsButton;
    }

    public void PlayButton()
    {
        EventSystem.current.SetSelectedGameObject(PlayFirstButton);
        LastSelected = PlayFirstButton;
    }

    public void PickTypesButton()
    {
        EventSystem.current.SetSelectedGameObject(PickTypeFirstButton);
        LastSelected = PickTypeFirstButton;
    }

    public void BackToMain()
    {
        EventSystem.current.SetSelectedGameObject(MenuFirstButton);
        LastSelected = MenuFirstButton;
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
