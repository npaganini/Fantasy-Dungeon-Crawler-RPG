using System.Text;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeSoFar;
    private float _timer;

    private void Start()
    {
        _timer = 0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        // OnGUI();
        int minutes = Mathf.FloorToInt(_timer / 60F);
        int seconds = Mathf.FloorToInt(_timer - minutes * 60);
        timeSoFar.SetText(new StringBuilder(minutes.ToString("00")  + ":" + seconds.ToString("00")));
    }

    // void OnGUI() {
    //     int minutes = Mathf.FloorToInt(_timer / 60F);
    //     int seconds = Mathf.FloorToInt(_timer - minutes * 60);
    //     string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
    //
    //     GUI.Label(new Rect(10,10,250,100), niceTime);
    // }
}
