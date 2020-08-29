using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSettings : MonoBehaviour
{
    private GameManager _gameManager;

    private TitleSceneManager _titleSceneManager;

    [SerializeField] private GameObject gameObjectPanelWindow;

    [SerializeField] private GameObject gameObjectImageButtonClose;

    [SerializeField] private GameObject gameObjectImageButtonFinish;

    [SerializeField] private GameObject gameObjectSliderMusic;
    
    [SerializeField] private GameObject gameObjectSliderSound;

    private Image _imageButtonClose;

    private Image _imageButtonFinish;

    private Slider _sliderMusic;

    private Slider _sliderSound;
    
    [SerializeField] private Sprite spriteImageButtonClose;

    [SerializeField] private Sprite spriteImageButtonCloseHover;

    [SerializeField] private Sprite spriteImageButtonFinish;

    [SerializeField] private Sprite spriteImageButtonFinishHover;

    private bool _isLoaded;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _titleSceneManager = GameObject.Find("TitleSceneManager").GetComponent<TitleSceneManager>();

        _imageButtonClose = gameObjectImageButtonClose.GetComponent<Image>();
        _imageButtonFinish = gameObjectImageButtonFinish.GetComponent<Image>();
        _sliderMusic = gameObjectSliderMusic.GetComponent<Slider>();
        _sliderSound = gameObjectSliderSound.GetComponent<Slider>();

        _isLoaded = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChangedSliderMusic()
    {
        float value = _sliderMusic.value;
        if (value <= -48.0f)
        {
            value = -80.0f;
        }

        ES3.Save("PsyberZoneMusicVolume", value);
        _gameManager.ChangeAudioVolume("Music", value);
    }

    public void OnValueChangedSliderSound()
    {
        float value = _sliderSound.value;
        if (value <= -48.0f)
        {
            value = -80.0f;
        }

        ES3.Save("PsyberZoneSoundVolume", value);
        _gameManager.ChangeAudioVolume("Sound", value);
    }

    public void OnMouseEnterImageButtonClose()
    {
        _imageButtonClose.sprite = spriteImageButtonCloseHover;
    }

    public void OnMouseExitImageButtonClose()
    {
        _imageButtonClose.sprite = spriteImageButtonClose;
    }

    public void OnMouseDownImageButtonClose()
    {
        _titleSceneManager.HideSettingMenu();
    }

    public void OnMouseEnterImageButtonFinish()
    {
        _imageButtonFinish.sprite = spriteImageButtonFinishHover;
    }

    public void OnMouseExitImageButtonFinish()
    {
        _imageButtonFinish.sprite = spriteImageButtonFinish;
    }

    public void OnMouseDownImageButtonFinish()
    {
        _titleSceneManager.HideSettingMenu();
    }

    public void Initialize()
    {
        StartCoroutine(CoroutineInitialize());
    }

    public void Terminated()
    {
        
    }

    IEnumerator CoroutineInitialize()
    {
        while (!_isLoaded)
        {
            yield return null;
        }

        _imageButtonClose.sprite = spriteImageButtonClose;
        _imageButtonFinish.sprite = spriteImageButtonFinish;
        
        _sliderMusic.value = _gameManager.GetMusicVolume();
        _sliderSound.value = _gameManager.GetSoundVolume();
    }
}
