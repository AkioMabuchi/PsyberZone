using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneButtons : MonoBehaviour
{
    private GameManager _gameManager;

    private TitleSceneManager _titleSceneManager;
    
    [SerializeField] private GameObject gameObjectImageButtonStart;

    [SerializeField] private GameObject gameObjectImageButtonTutorial;

    [SerializeField] private GameObject gameObjectImageButtonSettings;

    [SerializeField] private GameObject gameObjectImageButtonRanking;

    [SerializeField] private Sprite spriteImageButtonStart;
    [SerializeField] private Sprite spriteImageButtonStartHover;
    [SerializeField] private Sprite spriteImageButtonStartSelected;
    [SerializeField] private Sprite spriteImageButtonTutorial;
    [SerializeField] private Sprite spriteImageButtonTutorialHover;
    [SerializeField] private Sprite spriteImageButtonTutorialSelected;
    [SerializeField] private Sprite spriteImageButtonSettings;
    [SerializeField] private Sprite spriteImageButtonSettingsHover;
    [SerializeField] private Sprite spriteImageButtonRanking;
    [SerializeField] private Sprite spriteImageButtonRankingHover;

    private AudioSource[] _audioSources;

    private Image _imageButtonStart;

    private Image _imageButtonTutorial;

    private Image _imageButtonSettings;

    private Image _imageButtonRanking;

    private bool _isActive;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _titleSceneManager = GameObject.Find("TitleSceneManager").GetComponent<TitleSceneManager>();
        
        _audioSources = GetComponents<AudioSource>();

        _imageButtonStart = gameObjectImageButtonStart.GetComponent<Image>();
        _imageButtonTutorial = gameObjectImageButtonTutorial.GetComponent<Image>();
        _imageButtonSettings = gameObjectImageButtonSettings.GetComponent<Image>();
        _imageButtonRanking = gameObjectImageButtonRanking.GetComponent<Image>();

        _isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnterImageButtonStart()
    {
        if (_isActive)
        {
            _audioSources[0].time = 0.05f;
            _audioSources[0].Play();
            _imageButtonStart.sprite = spriteImageButtonStartHover;
        }
    }

    public void OnMouseExitImageButtonStart()
    {
        if (_isActive)
        {
            _imageButtonStart.sprite = spriteImageButtonStart;
        }
    }

    public void OnMouseDownImageButtonStart()
    {
        if (_isActive)
        {
            StartCoroutine(CoroutineOnClickImageButtonStart());
        }
    }

    public void OnMouseEnterImageButtonTutorial()
    {
        if (_isActive)
        {
            _audioSources[0].time = 0.05f;
            _audioSources[0].Play();
            _imageButtonTutorial.sprite = spriteImageButtonTutorialHover;
        }
    }

    public void OnMouseExitImageButtonTutorial()
    {
        if (_isActive)
        {
            _imageButtonTutorial.sprite = spriteImageButtonTutorial;
        }
    }

    public void OnMouseDownImageButtonTutorial()
    {
        if (_isActive)
        {
            StartCoroutine(CoroutineOnClickImageButtonTutorial());
        }
    }

    public void OnMouseEnterImageButtonSettings()
    {
        if (_isActive)
        {
            _audioSources[0].time = 0.05f;
            _audioSources[0].Play();
            _imageButtonSettings.sprite = spriteImageButtonSettingsHover;
        }
    }

    public void OnMouseExitImageButtonSettings()
    {
        if (_isActive)
        {
            _imageButtonSettings.sprite = spriteImageButtonSettings;
        }
    }

    public void OnMouseDownImageButtonSettings()
    {
        if (_isActive)
        {
            _audioSources[3].time = 0.0f;
            _audioSources[3].Play();
            _titleSceneManager.ShowSettingMenu();
        }
    }

    public void OnMouseEnterImageButtonRanking()
    {
        if (_isActive)
        {
            _audioSources[0].time = 0.05f;
            _audioSources[0].Play();
            _imageButtonRanking.sprite = spriteImageButtonRankingHover;
        }
    }

    public void OnMouseExitImageButtonRanking()
    {
        if (_isActive)
        {
            _imageButtonRanking.sprite = spriteImageButtonRanking;
        }
    }

    public void OnMouseDownImageButtonRanking()
    {
        if (_isActive)
        {
            _audioSources[3].time = 0.0f;
            _audioSources[3].Play();
            _titleSceneManager.ShowRankingBoard();
        }
    }
    IEnumerator CoroutineOnClickImageButtonStart()
    {
        _isActive = false;
        _imageButtonTutorial.color = new Color(1.0f,1.0f,1.0f,0.5f);
        _imageButtonSettings.color = new Color(1.0f,1.0f,1.0f,0.5f);
        _imageButtonRanking.color = new Color(1.0f,1.0f,1.0f,0.5f);

        _audioSources[1].time = 0.0f;
        _audioSources[1].Play();
        
        _imageButtonStart.sprite = spriteImageButtonStartSelected;
        for (int i = 0; i < 12; i++)
        {
            _imageButtonStart.color = new Color(0.5f,0.5f,0.5f);
            yield return new WaitForSeconds(0.05f);
            _imageButtonStart.color = new Color(1.0f,1.0f,1.0f);
            yield return new WaitForSeconds(0.05f);
        }
        _gameManager.GameStart(0);
    }

    IEnumerator CoroutineOnClickImageButtonTutorial()
    {
        _isActive = false;
        _imageButtonStart.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        _imageButtonSettings.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        _imageButtonRanking.color=new Color(1.0f,1.0f,1.0f,0.5f);

        _audioSources[2].time = 0.0f;
        _audioSources[2].Play();

        _imageButtonTutorial.sprite = spriteImageButtonTutorialSelected;
        for (int i = 0; i < 6; i++)
        {
            _imageButtonTutorial.color = new Color(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            _imageButtonTutorial.color = new Color(1.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(0.1f);
        }
        _gameManager.GameStart(1);
    }
}
