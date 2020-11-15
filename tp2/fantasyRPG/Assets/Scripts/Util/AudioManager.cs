using UnityEngine;
using System;
public class AudioManager : MonoBehaviorSingleton<AudioManager>
{
    private AudioSource[] sources;
    public AudioSource menuMusic;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!PlayerPrefs.HasKey("vol"))
        {
            PlayerPrefs.SetFloat("vol", 100f);
        }
        else
        {
            setVolume(PlayerPrefs.GetFloat("vol"));

        }
    }
    public void setVolume(float vol)
    {
        sources = FindObjectsOfType<AudioSource>();
        foreach(var s in sources)
        {
            s.volume = vol;
        }
        PlayerPrefs.SetFloat("vol", vol);
    }

}
