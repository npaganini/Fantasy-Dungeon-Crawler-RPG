using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource bossMusic;
    void Start()
    {
        bossMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBossMusic()
    {
        bossMusic.volume = PlayerPrefs.GetFloat("vol");
        bossMusic.Play();
    }
}
