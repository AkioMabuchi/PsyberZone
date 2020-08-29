using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainGameStateManager : MonoBehaviour
{
    private MainSceneManager _mainSceneManager;
    
    private CanvasMain _canvasMain;

    [SerializeField] private int maxLevel;

    [SerializeField] private int[] nextLevelScore;
    
    private AudioSource[] _audioSources;

    private int _score = 0;

    private int _life = 5;

    private int _level = 1;

    private int _positionBonus = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainSceneManager = GameObject.Find("MainSceneManager").GetComponent<MainSceneManager>();
        _canvasMain = GameObject.Find("CanvasMain").GetComponent<CanvasMain>();
        
        _audioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && _mainSceneManager.IsActive() && _level < maxLevel && _life > 0 && !_mainSceneManager.IsTutorial()) 
        {
            _level++;
            _audioSources[0].time = 0.0f;
            _audioSources[0].Play();
            _canvasMain.DrawLevelWithFlicker(_level);
        }
    }

    public void Retry()
    {
        _score = 0;
        _life = 5;
        _level = 1;
        
        _canvasMain.DrawScore(_score);
        _canvasMain.DrawLife(_life);
        _canvasMain.DrawLevel(_level);
    }
    public void AddScore(int s)
    {
        _score += s * _positionBonus;

        if (_score > 999999999)
        {
            _score = 999999999;
        }

        if (_score < 0)
        {
            _score = 0;
        }
        _canvasMain.DrawScore(_score);
        if (_score >= nextLevelScore[_level] && _level < maxLevel && !_mainSceneManager.IsTutorial())
        {
            _level++;
            _audioSources[0].time = 0.0f;
            _audioSources[0].Play();
            _canvasMain.DrawLevelWithFlicker(_level);
        }
    }

    public void DecreaseLife()
    {
        if (_life > 0)
        {
            _life--;
            _canvasMain.DrawLife(_life);
        }
    }

    public void RecoverLife()
    {
        if (_life < 8)
        {
            _canvasMain.DrawLifeWithFlicker(_life);
            _life++;
        }
    }
    public void SetPositionBonus(int s)
    {
        _positionBonus = s;
        _canvasMain.DrawPositionBonus(_positionBonus);
    }
    

    public int GetScore()
    {
        return _score;
    }

    public int GetLife()
    {
        return _life;
    }

    public int GetLevel()
    {
        return _level;
    }
}
