using UnityEngine;
using System;
public class AudioManager : MonoBehaviorSingleton<AudioManager>
{
    public Sound[] sounds;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    public float getVolume()
    {
        return sounds[0].source.volume;
    }
    public void changeVolume(float f)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = f;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }
    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return false;
        return s.source.isPlaying;
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Pause();
    }
}
