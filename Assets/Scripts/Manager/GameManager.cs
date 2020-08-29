using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Records
{
    public Record[] records;
}

[System.Serializable] 
public class Record
{
    public string playerId;
    public string name;
    public int score;
    public string date;
}
public class GameManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void OpenNewWindow(string url);
    
    private SoundManager _soundManager;
    private MusicManager _musicManager;
    
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private GameObject gameObjectImageCircle;

    [SerializeField] private GameObject gameObjectTextMeshProFps;

    [SerializeField] private string recordAddress;
    
    [SerializeField] private bool isProduction;

    private TextMeshProUGUI _textMeshProFps;
    
    private int _gameMode;

    private int _networkStatus; // 0:idle 1:wait 2:success 3:error

    private string _playerId;

    private Record[] _records;

    private float _musicVolume;

    private float _soundVolume;

    private float _accum;
    private int _frames;
    private float _timeLeft;
    
    // Start is called before the first frame update
    void Start()
    {
        _soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        _musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();

        _textMeshProFps = gameObjectTextMeshProFps.GetComponent<TextMeshProUGUI>();
        
        if (ES3.KeyExists("PsyberZonePlayerId"))
        {
            _playerId = ES3.Load<string>("PsyberZonePlayerId");
        }
        else
        {
            _playerId = GeneratePlayerId(32);
            ES3.Save<string>("PsyberZonePlayerId", _playerId);
        }
        
        if (ES3.KeyExists("PsyberZoneMusicVolume"))
        {
            _musicVolume = ES3.Load<float>("PsyberZoneMusicVolume");
            ChangeAudioVolume("Music", _musicVolume);
        }
        else
        {
            ES3.Save("PsyberZoneMusicVolume", 0.0f);
        }

        if (ES3.KeyExists("PsyberZoneSoundVolume"))
        {
            _soundVolume = ES3.Load<float>("PsyberZoneSoundVolume");
            ChangeAudioVolume("Sound", _soundVolume);
        }
        else
        {
            ES3.Save("PsyberZoneSoundVolume", 0.0f);
        }


        StartCoroutine(CoroutineIntroduction());
    }

    // Update is called once per frame
    void Update()
    {
        _timeLeft += Time.deltaTime;
        _accum += Time.timeScale / Time.deltaTime;
        _frames++;

        _textMeshProFps.text = (_accum / _frames).ToString("f2") + "fps";
    }

    public void ChangeAudioVolume(string floatName, float toValue)
    {
        if (floatName == "Music")
        {
            _musicVolume = toValue;
        }

        if (floatName == "Sound")
        {
            _soundVolume = toValue;
        }
        audioMixer.SetFloat(floatName, toValue);
    }

    public void GameStart(int modeCode)
    {
        _gameMode = modeCode;
        StartCoroutine(CoroutineGameStart());
    }

    public void ReturnToTitle()
    {
        StartCoroutine(CoroutineReturnToTitle());
    }

    public void ReturnToTitle2()
    {
        StartCoroutine(CoroutineReturnToTitle2());
    }
    public void SendRecord(string name, int score)
    {
        StartCoroutine(CoroutineSendRecord(name, score));
    }

    public void ReceiveRecords()
    {
        StartCoroutine(CoroutineReceiveRecords());
    }
    IEnumerator CoroutineIntroduction()
    {
        yield return new WaitForSeconds(0.2f);
        if (isProduction)
        {
            _musicManager.ChangeMusic(1);
            SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive);
            Scene newScene = SceneManager.GetSceneByName("TitleScene");
            while (!newScene.isLoaded)
            {
                yield return null;
            }

            SceneManager.SetActiveScene(newScene);
        }
    }
    IEnumerator CoroutineGameStart()
    {
        _musicManager.ChangeMusic(0);
        gameObjectImageCircle.transform.DOScale(new Vector3(4.0f, 4.0f, 1.0f), 1.0f);
        yield return new WaitForSeconds(1.0f);
        SceneManager.UnloadSceneAsync("TitleScene");
        SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneByName("MainScene");
        while (!newScene.isLoaded)
        {
            yield return null;
        }

        
        SceneManager.SetActiveScene(newScene);
        
        gameObjectImageCircle.transform.DOScale(new Vector3(0.0f, 0.0f, 1.0f), 1.0f);
    }

    IEnumerator CoroutineReturnToTitle()
    {
        _musicManager.ChangeMusic(0);
        SceneManager.UnloadSceneAsync("MainScene");
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneByName("TitleScene");
        while (!newScene.isLoaded)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(newScene);
    }

    IEnumerator CoroutineReturnToTitle2()
    {
        _musicManager.ChangeMusic(0);
        gameObjectImageCircle.transform.DOScale(new Vector3(4.0f, 4.0f, 1.0f), 1.0f);
        yield return new WaitForSeconds(1.0f);
        SceneManager.UnloadSceneAsync("MainScene");
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneByName("TitleScene");
        while (!newScene.isLoaded)
        {
            yield return null;
        }

        
        SceneManager.SetActiveScene(newScene);
        
        gameObjectImageCircle.transform.DOScale(new Vector3(0.0f, 0.0f, 1.0f), 1.0f);
    }
    public void Tweet(string content)
    {
        string url = "https://twitter.com/intent/tweet?text=" + UnityWebRequest.EscapeURL(content);
        
        #if UNITY_EDITOR
        Application.OpenURL(url);
        #elif UNITY_WEBGL
        OpenNewWindow(url);
        #else
        Application.OpenURL(url);
        #endif
    }
    
    public int GetGameMode()
    {
        return _gameMode;
    }

    public string GetPlayerId()
    {
        return _playerId;
    }

    public int GetNetworkStatus()
    {
        return _networkStatus;
    }

    public Record[] GetRecords()
    {
        return _records;
    }

    public float GetMusicVolume()
    {
        return _musicVolume;
    }

    public float GetSoundVolume()
    {
        return _soundVolume;
    }
    
    IEnumerator CoroutineSendRecord(string name, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_id", _playerId);
        form.AddField("name", name);
        form.AddField("score", score);

        UnityWebRequest request =
            UnityWebRequest.Post("https://records.akiomabuchi.com/records/" + recordAddress + "/send", form);

        _networkStatus = 1; // wait

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            _networkStatus = 3; // error
        }
        else
        {
            if (request.responseCode == 204)
            {
                _networkStatus = 2; // success
            }
            else
            {
                _networkStatus = 3; // error
            }
        }
    }

    IEnumerator CoroutineReceiveRecords()
    {
        UnityWebRequest request =
            UnityWebRequest.Get("https://records.akiomabuchi.com/records/" + recordAddress + "/receive");

        _networkStatus = 1; //wait

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            _networkStatus = 3; // error
        }
        else
        {
            if (request.responseCode == 200)
            {
                _networkStatus = 2; // success
                _records = JsonUtility.FromJson<Records>(request.downloadHandler.text).records;
            }
            else
            {
                _networkStatus = 3;
            }
        }
    }
    string GeneratePlayerId(int length)
    {
        string[] characters =
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
            "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
        };
        string r = "";
        for (int i = 0; i < length; i++)
        {
            r += characters[Random.Range(0, characters.Length)];
        }
        return r;
    }
}
