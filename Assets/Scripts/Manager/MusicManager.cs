using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClipsMusics = new AudioClip[4];

    private AudioSource _audioSource;

    private int _currentMusicChannel;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusic(int musicChannel)
    {
        if (_currentMusicChannel != musicChannel)
        {
            _currentMusicChannel = musicChannel;
            _audioSource.time = 0.0f;
            _audioSource.clip = _audioClipsMusics[musicChannel];
            _audioSource.Play();
        }
    }
}
