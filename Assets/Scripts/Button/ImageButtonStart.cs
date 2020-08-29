using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageButtonStart : MonoBehaviour
{
    private AudioSource[] _audioSources;

    private Image _image;

    [SerializeField] private Sprite spriteButtonImage;

    [SerializeField] private Sprite spriteButtonImageHover;

    private bool _isHover;

    private bool _isClickable = true;
    // Start is called before the first frame update
    void Start()
    {
        _audioSources = GetComponents<AudioSource>();
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHover && Input.GetMouseButton(0))
        {
            StartCoroutine(CoroutineClicked());
        }
    }

    public void Disable()
    {
        _isClickable = false;
    }

    private void OnMouseEnter()
    {
        if (_isClickable)
        {
            _isHover = true;
            _audioSources[0].time = 0.05f;
            _audioSources[0].Play();
        }
    }

    private void OnMouseExit()
    {
        if (_isClickable)
        {
            _isHover = false;
        }
    }

    IEnumerator CoroutineClicked()
    {
        _audioSources[1].time = 0.0f;
        _audioSources[1].Play();
        yield return new WaitForSeconds(1.0f);
    }
}
