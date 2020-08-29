using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRecords : MonoBehaviour
{
    private GameManager _gameManager;

    private MainSceneManager _mainSceneManager;

    private MainGameStateManager _mainGameStateManager;
    
    [SerializeField] private GameObject[] gameObjectsTextMeshProsBestScoreNumbers = new GameObject[9];
    [SerializeField] private GameObject[] gameObjectsTextMeshProsYourScoreNumbers = new GameObject[9];
    [SerializeField] private GameObject[] gameObjectsTextMeshProsRank = new GameObject[8];
    [SerializeField] private GameObject[] gameObjectsTextMeshProsName = new GameObject[8];
    [SerializeField] private GameObject[] gameObjectsTextMeshProsScore = new GameObject[8];
    [SerializeField] private GameObject[] gameObjectsTextMeshProsDate = new GameObject[8];
    [SerializeField] private GameObject gameObjectTextMehProMessage;
    
    [SerializeField] private GameObject gameObjectInputFieldName;

    [SerializeField] private GameObject gameObjectImageButtonClose;

    [SerializeField] private GameObject gameObjectImageButtonSend;

    [SerializeField] private GameObject gameObjectImageButtonArrowLeft;

    [SerializeField] private GameObject gameObjectImageButtonArrowRight;

    private TextMeshProUGUI[] _textMeshProsBestScoreNumbers = new TextMeshProUGUI[9];

    private TextMeshProUGUI[] _textMeshProsYourScoreNumbers = new TextMeshProUGUI[9];

    private TextMeshProUGUI[] _textMeshProsRank = new TextMeshProUGUI[8];

    private TextMeshProUGUI[] _textMeshProsName = new TextMeshProUGUI[8];

    private TextMeshProUGUI[] _textMeshProsScore = new TextMeshProUGUI[8];

    private TextMeshProUGUI[] _textMeshProsDate = new TextMeshProUGUI[8];

    private TextMeshProUGUI _textMeshProMessage;

    private TMP_InputField _inputFieldName;

    private Image _imageButtonClose;

    private Image _imageButtonSend;

    private Image _imageButtonArrowLeft;

    private Image _imageButtonArrowRight;

    [SerializeField] private Sprite spriteImageButtonClose;

    [SerializeField] private Sprite spriteImageButtonCloseHover;

    [SerializeField] private Sprite spriteImageButtonSend;

    [SerializeField] private Sprite spriteImageButtonSendHover;

    [SerializeField] private Sprite spriteImageButtonArrowLeft;

    [SerializeField] private Sprite spriteImageButtonArrowLeftHover;

    [SerializeField] private Sprite spriteImageButtonArrowRight;

    [SerializeField] private Sprite spriteImageButtonArrowRightHover;

    private bool _isActiveImageButtonSend;

    private bool _isActiveImageButtonArrowLeft;

    private bool _isActiveImageButtonArrowRight;

    private string _name;

    private int _score;

    private Record[] _records;

    private int _currentIndex;

    private bool _isLoaded;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _mainSceneManager = GameObject.Find("MainSceneManager").GetComponent<MainSceneManager>();
        _mainGameStateManager = GameObject.Find("MainGameStateManager").GetComponent<MainGameStateManager>();

        for (int i = 0; i < 9; i++)
        {
            _textMeshProsBestScoreNumbers[i] =
                gameObjectsTextMeshProsBestScoreNumbers[i].GetComponent<TextMeshProUGUI>();
            _textMeshProsYourScoreNumbers[i] =
                gameObjectsTextMeshProsYourScoreNumbers[i].GetComponent<TextMeshProUGUI>();
        }

        for (int i = 0; i < 8; i++)
        {
            _textMeshProsRank[i] = gameObjectsTextMeshProsRank[i].GetComponent<TextMeshProUGUI>();
            _textMeshProsName[i] = gameObjectsTextMeshProsName[i].GetComponent<TextMeshProUGUI>();
            _textMeshProsScore[i] = gameObjectsTextMeshProsScore[i].GetComponent<TextMeshProUGUI>();
            _textMeshProsDate[i] = gameObjectsTextMeshProsDate[i].GetComponent<TextMeshProUGUI>();
        }

        _textMeshProMessage = gameObjectTextMehProMessage.GetComponent<TextMeshProUGUI>();
        
        _inputFieldName = gameObjectInputFieldName.GetComponent<TMP_InputField>();
        _imageButtonClose = gameObjectImageButtonClose.GetComponent<Image>();
        _imageButtonSend = gameObjectImageButtonSend.GetComponent<Image>();
        _imageButtonArrowLeft = gameObjectImageButtonArrowLeft.GetComponent<Image>();
        _imageButtonArrowRight = gameObjectImageButtonArrowRight.GetComponent<Image>();

        _isLoaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        _mainSceneManager.HideRecords();
    }

    public void OnMouseEnterImageButtonSend()
    {
        if (_isActiveImageButtonSend)
        {
            _imageButtonSend.sprite = spriteImageButtonSendHover;
        }
    }

    public void OnMouseExitImageButtonSend()
    {
        if (_isActiveImageButtonSend)
        {
            _imageButtonSend.sprite = spriteImageButtonSend;
        }
    }

    public void OnMouseDownImageButtonSend()
    {
        if (_isActiveImageButtonSend)
        {
            StartCoroutine(CoroutineOnMouseDownImageButtonSend());
        }
    }

    public void OnMouseEnterImageButtonArrowLeft()
    {
        if (_isActiveImageButtonArrowLeft)
        {
            _imageButtonArrowLeft.sprite = spriteImageButtonArrowLeftHover;
        }
    }

    public void OnMouseExitImageButtonArrowLeft()
    {
        if (_isActiveImageButtonArrowLeft)
        {
            _imageButtonArrowLeft.sprite = spriteImageButtonArrowLeft;
        }
    }

    public void OnMouseDownImageButtonArrowLeft()
    {
        if (_isActiveImageButtonArrowLeft)
        {
            _currentIndex -= 8;
            DisplayRecords();
        }
    }

    public void OnMouseEnterImageButtonArrowRight()
    {
        if (_isActiveImageButtonArrowRight)
        {
            _imageButtonArrowRight.sprite = spriteImageButtonArrowRightHover;
        }
    }

    public void OnMouseExitImageButtonArrowRight()
    {
        if (_isActiveImageButtonArrowRight)
        {
            _imageButtonArrowRight.sprite = spriteImageButtonArrowRight;
        }
    }

    public void OnMouseDownImageButtonArrowRight()
    {
        if (_isActiveImageButtonArrowRight)
        {
            _currentIndex += 8;
            DisplayRecords();
        }
    }

    public void Initialize()
    {
        StartCoroutine(CoroutineInitialize());
    }

    public void Terminated()
    {
        
    }

    public void OnValueChangedInputFieldName()
    {
        _name = _inputFieldName.text;
        if (_inputFieldName.text == "")
        {
            _isActiveImageButtonSend = false;
            _imageButtonSend.color = new Color(1.0f,1.0f,1.0f,0.5f);
        }
        else
        {
            _isActiveImageButtonSend = true;
            _imageButtonSend.color=new Color(1.0f,1.0f,1.0f,1.0f);
        }
    }

    IEnumerator CoroutineInitialize()
    {
        while (!_isLoaded)
        {
            yield return null;
        }

        _imageButtonClose.sprite = spriteImageButtonClose;
        
        _isActiveImageButtonSend = false;
        _imageButtonSend.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        _imageButtonSend.sprite = spriteImageButtonSend;
        
        _isActiveImageButtonArrowLeft = false;
        _imageButtonArrowLeft.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        _imageButtonArrowLeft.sprite = spriteImageButtonArrowLeft;
        
        _isActiveImageButtonArrowRight = false;
        _imageButtonArrowRight.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        _imageButtonArrowRight.sprite = spriteImageButtonArrowRight;
        
        _inputFieldName.text = "";
        _inputFieldName.interactable = false;
        
        _score = _mainGameStateManager.GetScore();

        for (int i = 0; i < 9; i++)
        {
            _textMeshProsBestScoreNumbers[i].text = "-";

            if (_score / (int) Mathf.Pow(10, i) == 0 && i > 0) 
            {
                _textMeshProsYourScoreNumbers[i].text = "・";
            }
            else
            {
                _textMeshProsYourScoreNumbers[i].text = (_score / (int) Mathf.Pow(10, i) % 10).ToString(); 
            }
        }
        
        DisplayMessage("データ取得中・・・");
        _gameManager.ReceiveRecords();
        while (_gameManager.GetNetworkStatus() == 1)
        {
            yield return null;
        }

        switch (_gameManager.GetNetworkStatus())
        {
            case 2:
                _inputFieldName.interactable = true;
                _records = _gameManager.GetRecords();
                DisplayRecords();
                foreach (Record record in _records)
                {
                    if (record.playerId == _gameManager.GetPlayerId())
                    {
                        _inputFieldName.text = record.name;
                        for (int i = 0; i < 9; i++)
                        {
                            if (record.score / (int) Mathf.Pow(10, i) == 0 && i > 0) 
                            {
                                _textMeshProsBestScoreNumbers[i].text = "・";
                            }
                            else
                            {
                                _textMeshProsBestScoreNumbers[i].text =
                                    (record.score / (int) Mathf.Pow(10, i) % 10).ToString();
                            }
                        }
                    }
                }

                _currentIndex = 0;
                break;
            case 3:
                DisplayMessage("データの取得に失敗しました");
                break;
        }
    }

    IEnumerator CoroutineOnMouseDownImageButtonSend()
    {
        DisplayMessage("データ送信中・・・");
        _gameManager.SendRecord(_name, _score);
        while (_gameManager.GetNetworkStatus() == 1)
        {
            yield return null;
        }

        switch (_gameManager.GetNetworkStatus())
        {
            case 2:
                
                _gameManager.ReceiveRecords();
                while (_gameManager.GetNetworkStatus() == 1)
                {
                    yield return null;
                }

                _records = _gameManager.GetRecords();
                _currentIndex = 0;
                for (int i = 0; i < _records.Length; i++)
                {
                    if (_records[i].playerId == _gameManager.GetPlayerId())
                    {
                        _currentIndex = i / 8 * 8;
                    }
                }
                foreach (Record record in _records)
                {
                    if (record.playerId == _gameManager.GetPlayerId())
                    {
                        _inputFieldName.text = record.name;
                        for (int i = 0; i < 9; i++)
                        {
                            if (record.score / (int) Mathf.Pow(10, i) == 0 && i > 0) 
                            {
                                _textMeshProsBestScoreNumbers[i].text = "・";
                            }
                            else
                            {
                                _textMeshProsBestScoreNumbers[i].text =
                                    (record.score / (int) Mathf.Pow(10, i) % 10).ToString();
                            }
                        }
                    }
                }
                DisplayRecords();
                break;
            case 3:
                DisplayMessage("データの送信に失敗しました");
                break;
        }
    }

    void DisplayRecords()
    {
        for (int i = 0; i < 8; i++)
        {
            int a = i + _currentIndex;
            if (a >= _records.Length)
            {
                _textMeshProsRank[i].text = "";
                _textMeshProsName[i].text = "";
                _textMeshProsScore[i].text = "";
                _textMeshProsDate[i].text = "";
            }
            else
            {
                _textMeshProsRank[i].text = (a + 1).ToString();
                _textMeshProsName[i].text = _records[a].name;
                _textMeshProsScore[i].text = _records[a].score.ToString();
                _textMeshProsDate[i].text = _records[a].date;
                if (_gameManager.GetPlayerId() == _records[a].playerId)
                {
                    _textMeshProsRank[i].color = Color.yellow;
                    _textMeshProsName[i].color = Color.yellow;
                    _textMeshProsScore[i].color = Color.yellow;
                    _textMeshProsDate[i].color = Color.yellow;
                }
                else
                {
                    _textMeshProsRank[i].color = Color.white;
                    _textMeshProsName[i].color = Color.white;
                    _textMeshProsScore[i].color = Color.white;
                    _textMeshProsDate[i].color = Color.white;
                }
            }
        }

        _textMeshProMessage.text = "";

        if (_currentIndex == 0)
        {
            _isActiveImageButtonArrowLeft = false;
            _imageButtonArrowLeft.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            _imageButtonArrowLeft.sprite = spriteImageButtonArrowLeft;
        }
        else
        {
            _isActiveImageButtonArrowLeft = true;
            _imageButtonArrowLeft.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        if (_currentIndex + 8 >= _records.Length)
        {
            _isActiveImageButtonArrowRight = false;
            _imageButtonArrowRight.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            _imageButtonArrowRight.sprite = spriteImageButtonArrowRight;
        }
        else
        {
            _isActiveImageButtonArrowRight = true;
            _imageButtonArrowRight.color=new Color(1.0f,1.0f,1.0f,1.0f);
        }
    }

    void DisplayMessage(string message)
    {
        for (int i = 0; i < 8; i++)
        {
            _textMeshProsRank[i].text = "";
            _textMeshProsName[i].text = "";
            _textMeshProsScore[i].text = "";
            _textMeshProsDate[i].text = "";
        }

        _textMeshProMessage.text = message;
    }
}
