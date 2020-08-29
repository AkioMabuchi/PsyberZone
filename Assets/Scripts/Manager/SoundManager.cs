using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] _audioSources;
    // Start is called before the first frame update
    void Start()
    {
        _audioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int index)
    {
        _audioSources[index].time = 0.0f;
        _audioSources[index].Play();
    }
}
