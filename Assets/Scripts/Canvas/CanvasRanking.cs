using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CanvasRanking : MonoBehaviour
{
    private GameManager _gameManager;

    private TitleSceneManager _titleSceneManager;
    
    [SerializeField] private GameObject[] gameObjectsTextMeshProsRank = new GameObject[14];
    [SerializeField] private GameObject[] gameObjectsTextMeshProsName = new GameObject[14];
    [SerializeField] private GameObject[] gameObjectsTextMeshProsScore = new GameObject[14];
    [SerializeField] private GameObject[] gameObjectsTextMeshProsDate = new GameObject[14];
    [SerializeField] private GameObject gameObjectTextMehProMessage;
    
    [SerializeField] private GameObject gameObjectImageButtonClose;

    [SerializeField] private GameObject gameObjectImageButtonArrowLeft;

    [SerializeField] private GameObject gameObjectImageButtonArrowRight;
    
    private TextMeshProUGUI[] _textMeshProsRank = new TextMeshProUGUI[14];

    private TextMeshProUGUI[] _textMeshProsName = new TextMeshProUGUI[14];

    private TextMeshProUGUI[] _textMeshProsScore = new TextMeshProUGUI[14];

    private TextMeshProUGUI[] _textMeshProsDate = new TextMeshProUGUI[14];

    private TextMeshProUGUI _textMeshProMessage;

    private Image _imageButtonClose;

    private Image _imageButtonArrowLeft;

    private Image _imageButtonArrowRight;
    
    [SerializeField] private Sprite spriteImageButtonClose;

    [SerializeField] private Sprite spriteImageButtonCloseHover;

    [SerializeField] private Sprite spriteImageButtonArrowLeft;

    [SerializeField] private Sprite spriteImageButtonArrowLeftHover;

    [SerializeField] private Sprite spriteImageButtonArrowRight;

    [SerializeField] private Sprite spriteImageButtonArrowRightHover;
    
    private bool _isActiveImageButtonArrowLeft;

    private bool _isActiveImageButtonArrowRight;

    private Record[] _records;

    private int _currentIndex;

    private bool _isLoaded;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _titleSceneManager = GameObject.Find("TitleSceneManager").GetComponent<TitleSceneManager>();
        
        for (int i = 0; i < 14; i++)
        {
            _textMeshProsRank[i] = gameObjectsTextMeshProsRank[i].GetComponent<TextMeshProUGUI>();
            _textMeshProsName[i] = gameObjectsTextMeshProsName[i].GetComponent<TextMeshProUGUI>();
            _textMeshProsScore[i] = gameObjectsTextMeshProsScore[i].GetComponent<TextMeshProUGUI>();
            _textMeshProsDate[i] = gameObjectsTextMeshProsDate[i].GetComponent<TextMeshProUGUI>();
        }

        _textMeshProMessage = gameObjectTextMehProMessage.GetComponent<TextMeshProUGUI>();
        
        _imageButtonClose = gameObjectImageButtonClose.GetComponent<Image>();
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
        _titleSceneManager.HideRankingBoard();
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
            _currentIndex -= 14;
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
            _currentIndex += 14;
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
    
    IEnumerator CoroutineInitialize()
    {
        while (!_isLoaded)
        {
            yield return null;
        }

        _imageButtonClose.sprite = spriteImageButtonClose;
        
        _isActiveImageButtonArrowLeft = false;
        _imageButtonArrowLeft.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        _imageButtonArrowLeft.sprite = spriteImageButtonArrowLeft;
        
        _isActiveImageButtonArrowRight = false;
        _imageButtonArrowRight.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        _imageButtonArrowRight.sprite = spriteImageButtonArrowRight;
        
        DisplayMessage("データ取得中・・・");
        _gameManager.ReceiveRecords();
        while (_gameManager.GetNetworkStatus() == 1)
        {
            yield return null;
        }

        switch (_gameManager.GetNetworkStatus())
        {
            case 2:
                _currentIndex = 0;
                _records = _gameManager.GetRecords();
                DisplayRecords();
                break;
            case 3:
                DisplayMessage("データの取得に失敗しました");
                break;
        }
    }
    
        void DisplayRecords()
    {
        for (int i = 0; i < 14; i++)
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

        if (_currentIndex + 14 >= _records.Length)
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
        for (int i = 0; i < 14; i++)
        {
            _textMeshProsRank[i].text = "";
            _textMeshProsName[i].text = "";
            _textMeshProsScore[i].text = "";
            _textMeshProsDate[i].text = "";
        }

        _textMeshProMessage.text = message;
    }
}
