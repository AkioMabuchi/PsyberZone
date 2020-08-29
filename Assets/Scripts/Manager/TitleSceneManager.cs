using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    private GameManager _gameManager;

    private MusicManager _musicManager;

    [SerializeField] private GameObject gameObjectCanvasSetting;

    [SerializeField] private GameObject gameObjectCanvasRanking;

    private CanvasSettings _canvasSettings;
    private CanvasRanking _canvasRanking;

    private AudioSource[] _audioSources;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();

        _canvasSettings = gameObjectCanvasSetting.GetComponent<CanvasSettings>();
        _canvasRanking = gameObjectCanvasRanking.GetComponent<CanvasRanking>();

        _audioSources = GetComponents<AudioSource>();
        
        StartCoroutine(CoroutineIntroduction());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CoroutineIntroduction()
    {
        yield return new WaitForSeconds(1.0f);
        _musicManager.ChangeMusic(1);
    }

    public void ShowSettingMenu()
    {
        gameObjectCanvasSetting.SetActive(true);
        _canvasSettings.Initialize();
    }

    public void HideSettingMenu()
    {
        _audioSources[0].time = 0.05f;
        _audioSources[0].Play();
        _canvasRanking.Terminated();
        gameObjectCanvasSetting.SetActive(false);
    }

    public void ShowRankingBoard()
    {
        gameObjectCanvasRanking.SetActive(true);
        _canvasRanking.Initialize();
    }

    public void HideRankingBoard()
    {
        _audioSources[0].time = 0.05f;
        _audioSources[0].Play();
        _canvasRanking.Terminated();
        gameObjectCanvasRanking.SetActive(false);
    }

    public void SoundTest()
    {
        _audioSources[1].time = 0.02f;
        _audioSources[1].Play();
    }
}
