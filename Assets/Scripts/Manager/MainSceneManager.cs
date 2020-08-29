using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    private GameManager _gameManager;

    private MusicManager _musicManager;

    private MainGameStateManager _mainGameStateManager;

    private MainSceneButtons _mainSceneButtons;

    private SpawnManager _spawnManager;

    private Player _player;

    [SerializeField] private GameObject gameObjectCanvasMain;
    [SerializeField] private GameObject gameObjectCanvasGameOver;
    [SerializeField] private GameObject gameObjectCanvasRecords;

    private CanvasMain _canvasMain;
    private CanvasGameOver _canvasGameOver;
    private CanvasRecords _canvasRecords;

    private AudioSource _audioSource;

    private bool _isActive;
    private bool _isGameOver;
    private bool _isTutorial;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        _mainGameStateManager = GameObject.Find("MainGameStateManager").GetComponent<MainGameStateManager>();
        _mainSceneButtons = GameObject.Find("MainSceneButtons").GetComponent<MainSceneButtons>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        
        _canvasMain = gameObjectCanvasMain.GetComponent<CanvasMain>();
        _canvasGameOver = gameObjectCanvasGameOver.GetComponent<CanvasGameOver>();
        _canvasRecords = gameObjectCanvasRecords.GetComponent<CanvasRecords>();

        _audioSource = GetComponent<AudioSource>();
        
        StartCoroutine(CoroutineIntroduction());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && _isActive && !_isGameOver)
        {
            _gameManager.ReturnToTitle();
        }
    }

    IEnumerator CoroutineIntroduction()
    {
        yield return new WaitForSeconds(2.0f);
        _isActive = true;
        switch (_gameManager.GetGameMode())
        {
            case 0: // Normal Mode
                _isTutorial = false;
                _spawnManager.ActivateCoroutineNormalMode();
                _musicManager.ChangeMusic(2);
                break;
            case 1: // Tutorial Mode
                _isTutorial = true;
                _spawnManager.ActivateCoroutineTutorialMode();
                _musicManager.ChangeMusic(3);
                break;
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
        _musicManager.ChangeMusic(0);
        _mainSceneButtons.Initialize();
        
        gameObjectCanvasGameOver.SetActive(true);
        _canvasGameOver.Initialize();
    }

    public void Retry()
    {
        StartCoroutine(CoroutineRetry());
    }

    public void Tweet()
    {
        string content = "『Psyber Zone』で" + _mainGameStateManager.GetScore() +
                         "点取得したよ！\n\n#PsyberZone #unityroom #unity1week\nhttps://unityroom.com/games/psyberzone";
        _gameManager.Tweet(content);

    }

    public void ShowRecords()
    {
        gameObjectCanvasRecords.SetActive(true);
        _canvasRecords.Initialize();
    }

    public void HideRecords()
    {
        _audioSource.time = 0.05f;
        _audioSource.Play();
        
        _canvasRecords.Terminated();
        gameObjectCanvasRecords.SetActive(false);
    }

    IEnumerator CoroutineRetry()
    {
        _isGameOver = false;
        _mainGameStateManager.Retry();
        _spawnManager.Retry();
        _player.Retry();
        
        _canvasGameOver.Terminated();
        gameObjectCanvasGameOver.SetActive(false);
        
        yield return new WaitForSeconds(1.0f);
        
        _musicManager.ChangeMusic(2);
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public bool IsTutorial()
    {
        return _isTutorial;
    }
}
