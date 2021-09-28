using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource audiosource;
    // Start is called before the first frame update
    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    void Start()
    {

        if(audiosource != null)
        {
            audiosource.volume = PlayerPrefs.GetFloat("vol");
            audiosource.Play();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
