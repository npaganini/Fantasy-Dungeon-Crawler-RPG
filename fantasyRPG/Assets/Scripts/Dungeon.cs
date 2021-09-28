using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.setVolume(PlayerPrefs.GetFloat("vol"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
