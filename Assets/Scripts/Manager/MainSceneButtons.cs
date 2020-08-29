using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneButtons : MonoBehaviour
{
    private GameManager _gameManager;

    private MainSceneManager _mainSceneManager;

    [SerializeField] private GameObject gameObjectImageButtonTweet;

    [SerializeField] private GameObject gameObjectImageButtonRecords;

    [SerializeField] private GameObject gameObjectImageButtonRetry;

    [SerializeField] private GameObject gameObjectImageButtonTitle;

    [SerializeField] private Sprite spriteImageButtonTweet;
    [SerializeField] private Sprite spriteImageButtonTweetHover;
    [SerializeField] private Sprite spriteImageButtonRecords;
    [SerializeField] private Sprite spriteImageButtonRecordsHover;
    [SerializeField] private Sprite spriteImageButtonRetry;
    [SerializeField] private Sprite spriteImageButtonRetryHover;
    [SerializeField] private Sprite spriteImageButtonRetrySelected;
    [SerializeField] private Sprite spriteImageButtonTitle;
    [SerializeField] private Sprite spriteImageButtonTitleHover;

    private AudioSource[] _audioSources;
    
    private Image _imageButtonTweet;
    private Image _imageButtonRecords;
    private Image _imageButtonRetry;
    private Image _imageButtonTitle;

    private bool _isLoaded;
    
    private bool _isActive;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _mainSceneManager = GameObject.Find("MainSceneManager").GetComponent<MainSceneManager>();

        _audioSources = GetComponents<AudioSource>();
        
        _imageButtonTweet = gameObjectImageButtonTweet.GetComponent<Image>();
        _imageButtonRecords = gameObjectImageButtonRecords.GetComponent<Image>();
        _imageButtonRetry = gameObjectImageButtonRetry.GetComponent<Image>();
        _imageButtonTitle = gameObjectImageButtonTitle.GetComponent<Image>();

        _isLoaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        StartCoroutine(CoroutineInitialize());
    }
    
    public void OnMouseEnterImageButtonTweet()
    {
        if (_isActive)
        {
            _audioSources[0].time = 0.05f;
            _audioSources[0].Play();
            _imageButtonTweet.sprite = spriteImageButtonTweetHover;
        }
    }

    public void OnMouseExitImageButtonTweet()
    {
        if (_isActive)
        {
            _imageButtonTweet.sprite = spriteImageButtonTweet;
        }
    }

    public void OnMouseDownImageButtonTweet()
    {
        if (_isActive)
        {
            _mainSceneManager.Tweet();
        }
    }

    public void OnMouseEnterImageButtonRecords()
    {
        if (_isActive)
        {
            _audioSources[0].time = 0.05f;
            _audioSources[0].Play();
            _imageButtonRecords.sprite = spriteImageButtonRecordsHover;
        }
    }

    public void OnMouseExitImageButtonRecords()
    {
        if (_isActive)
        {
            _imageButtonRecords.sprite = spriteImageButtonRecords;
        }
    }

    public void OnMouseDownImageButtonRecords()
    {
        if (_isActive)
        {
            _audioSources[3].time = 0.0f;
            _audioSources[3].Play();
            _mainSceneManager.ShowRecords();
        }
    }

    public void OnMouseEnterImageButtonRetry()
    {
        if (_isActive)
        {
            _audioSources[0].time = 0.05f;
            _audioSources[0].Play();
            _imageButtonRetry.sprite = spriteImageButtonRetryHover;
        }
    }

    public void OnMouseExitImageButtonRetry()
    {
        if (_isActive)
        {
            _imageButtonRetry.sprite = spriteImageButtonRetry;
        }
    }

    public void OnMouseDownImageButtonRetry()
    {
        if (_isActive)
        {
            StartCoroutine(CoroutineOnClickImageButtonRetry());
        }
    }

    public void OnMouseEnterImageButtonTitle()
    {
        if (_isActive)
        {
            _audioSources[0].time = 0.05f;
            _audioSources[0].Play();
            _imageButtonTitle.sprite = spriteImageButtonTitleHover;
        }
    }

    public void OnMouseExitImageButtonTitle()
    {
        if (_isActive)
        {
            _imageButtonTitle.sprite = spriteImageButtonTitle;
        }
    }

    public void OnMouseDownImageButtonTitle()
    {
        if (_isActive)
        {
            _isActive = false;
            _audioSources[1].time = 0.0f;
            _audioSources[1].Play();
            
            _gameManager.ReturnToTitle2();
        }
    }

    IEnumerator CoroutineInitialize()
    {
        while (!_isLoaded)
        {
            yield return null;
        }

        _imageButtonTweet.sprite = spriteImageButtonTweet;
        _imageButtonTweet.color = new Color(1.0f, 1.0f, 1.0f,1.0f);
        
        _imageButtonRecords.sprite = spriteImageButtonRecords;
        _imageButtonRecords.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        
        _imageButtonRetry.sprite = spriteImageButtonRetry;
        _imageButtonRetry.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        
        _imageButtonTitle.sprite = spriteImageButtonTitle;
        _imageButtonTitle.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        
        _isActive = true;
    }
    IEnumerator CoroutineOnClickImageButtonRetry()
    {
        _isActive = false;
        _imageButtonTweet.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        _imageButtonRecords.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        _imageButtonTitle.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        
        _audioSources[2].time = 0.0f;
        _audioSources[2].Play();

        _imageButtonRetry.sprite = spriteImageButtonRetrySelected;
        for (int i = 0; i < 6; i++)
        {
            _imageButtonRetry.color = new Color(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            _imageButtonRetry.color = new Color(1.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(0.1f);
        }
        _mainSceneManager.Retry();
    }
}
