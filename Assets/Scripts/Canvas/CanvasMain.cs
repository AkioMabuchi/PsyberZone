using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CanvasMain : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectTextMeshProScore;

    [SerializeField] private GameObject[] gameObjectsTextMeshProsScoreNumbers = new GameObject[9];

    [SerializeField] private GameObject gameObjectTextMeshProLife;

    [SerializeField] private GameObject[] gameObjectsTextMeshProsLifeGauges = new GameObject[8];

    [SerializeField] private GameObject gameObjectTextMeshProLevel;

    [SerializeField] private GameObject[] gameObjectsTextMeshProsLevelNumbers = new GameObject[3];

    [SerializeField] private GameObject gameObjectTextMeshProPositionBonus;

    [SerializeField] private GameObject gameObjectTextMeshProPositionBonusNumber;

    [SerializeField] private GameObject gameObjectTextMeshProMessage;

    private TextMeshProUGUI _textMeshProScore;

    private TextMeshProUGUI[] _textMeshProsScoreNumbers = new TextMeshProUGUI[9];

    private TextMeshProUGUI _textMeshProLife;

    private TextMeshProUGUI[] _textMeshProsLifeGauges = new TextMeshProUGUI[8];

    private TextMeshProUGUI _textMeshProLevel;

    private TextMeshProUGUI[] _textMeshProsLevelNumbers = new TextMeshProUGUI[3];

    private TextMeshProUGUI _textMeshProPositionBonus;

    private TextMeshProUGUI _textMeshProPositionBonusNumber;

    private TextMeshProUGUI _textMeshProMessage;

    private Coroutine _coroutineDrawLevelWithFlicker;

    private Coroutine _coroutineDrawLifeWithFlicker;

    [SerializeField] private int maxLevel;
    
    private float _leftUIFading = 1.0f;
    private float _rightUIFading = 1.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _textMeshProScore = gameObjectTextMeshProScore.GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < 9; i++)
        {
            _textMeshProsScoreNumbers[i] = gameObjectsTextMeshProsScoreNumbers[i].GetComponent<TextMeshProUGUI>();
        }

        _textMeshProLife = gameObjectTextMeshProLife.GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < 8; i++)
        {
            _textMeshProsLifeGauges[i] = gameObjectsTextMeshProsLifeGauges[i].GetComponent<TextMeshProUGUI>();
        }

        _textMeshProLevel = gameObjectTextMeshProLevel.GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < 3; i++)
        {
            _textMeshProsLevelNumbers[i] = gameObjectsTextMeshProsLevelNumbers[i].GetComponent<TextMeshProUGUI>();
        }

        _textMeshProPositionBonus = gameObjectTextMeshProPositionBonus.GetComponent<TextMeshProUGUI>();
        _textMeshProPositionBonusNumber = gameObjectTextMeshProPositionBonusNumber.GetComponent<TextMeshProUGUI>();

        _textMeshProMessage = gameObjectTextMeshProMessage.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _textMeshProScore.color=new Color(1.0f,1.0f,1.0f,_rightUIFading);
        for (int i = 0; i < 9; i++)
        {
            _textMeshProsScoreNumbers[i].color=new Color(1.0f,1.0f,1.0f,_rightUIFading);
        }
        _textMeshProLife.color = new Color(1.0f, 1.0f, 1.0f, _leftUIFading);
        for (int i = 0; i < 8; i++)
        {
            _textMeshProsLifeGauges[i].color = new Color(1.0f, 1.0f, 1.0f, _leftUIFading);
        }
        _textMeshProLevel.color=new Color(1.0f,1.0f,1.0f,_leftUIFading);
        for (int i = 0; i < 3; i++)
        {
            _textMeshProsLevelNumbers[i].color=new Color(1.0f,1.0f,1.0f,_leftUIFading);
        }

        _textMeshProPositionBonus.color = new Color(1.0f, 1.0f, 1.0f, _rightUIFading);
        _textMeshProPositionBonusNumber.color = new Color(1.0f, 1.0f, 1.0f, _rightUIFading);
    }

    public void DrawScore(int s)
    {
        for (int i = 0; i < 9; i++)
        {
            if (s / (int) Mathf.Pow(10, i) == 0 && i > 0) 
            {
                _textMeshProsScoreNumbers[i].text = "・";
            }
            else
            {
                _textMeshProsScoreNumbers[i].text = (s / (int) Mathf.Pow(10, i) % 10).ToString();
            }
        }
    }

    public void DrawLife(int s)
    {
        for (int i = 0; i < 8; i++)
        {
            if (s > i)
            {
                _textMeshProsLifeGauges[i].text = "I";
            }
            else
            {
                _textMeshProsLifeGauges[i].text = "";
            }
        }
    }

    public void DrawLevel(int s)
    {
        if (s >= maxLevel)
        {
            _textMeshProsLevelNumbers[0].text = "X";
            _textMeshProsLevelNumbers[1].text = "A";
            _textMeshProsLevelNumbers[2].text = "M";
        }
        else if (s >= 10)
        {
            _textMeshProsLevelNumbers[0].text = "";
            _textMeshProsLevelNumbers[1].text = (s % 10).ToString();
            _textMeshProsLevelNumbers[2].text = (s / 10 % 10).ToString();
        }
        else
        {
            _textMeshProsLevelNumbers[0].text = "";
            _textMeshProsLevelNumbers[1].text = "";
            _textMeshProsLevelNumbers[2].text = (s % 10).ToString();
        }
    }

    public void DrawLevelWithFlicker(int s)
    {
        if (_coroutineDrawLevelWithFlicker != null) StopCoroutine(_coroutineDrawLevelWithFlicker);
        _coroutineDrawLevelWithFlicker = StartCoroutine(CoroutineDrawLevelWithFlicker(s));
    }

    public void DrawLifeWithFlicker(int s)
    {
        if (_coroutineDrawLifeWithFlicker != null) StopCoroutine(_coroutineDrawLifeWithFlicker);
        StartCoroutine(CoroutineDrawLifeWithFlicker(s));
    }
    
    public void DrawPositionBonus(int s)
    {
        _textMeshProPositionBonusNumber.text = s.ToString();
    }

    public void FadeLeftUIs(bool s)
    {
        if (s)
        {
            _leftUIFading -= 0.08f;
            if (_leftUIFading < 0.2f)
            {
                _leftUIFading = 0.2f;
            }
        }
        else
        {
            _leftUIFading += 0.08f;
            if (_leftUIFading > 1.0f)
            {
                _leftUIFading = 1.0f;
            }
        }
    }

    public void FadeRightUIs(bool s)
    {
        if (s)
        {
            _rightUIFading -= 0.08f;
            if (_rightUIFading < 0.2f)
            {
                _rightUIFading = 0.2f;
            }
        }
        else
        {
            _rightUIFading += 0.08f;
            if (_rightUIFading > 1.0f)
            {
                _rightUIFading = 1.0f;
            }
        }
    }

    public void ShowMessage(string message)
    {

        StartCoroutine(CoroutineShowMessage(message));
    }

    IEnumerator CoroutineDrawLevelWithFlicker(int s)
    {
        string[] textNumbers = new string[3];
        if (s >= maxLevel)
        {
            textNumbers[0] = "X";
            textNumbers[1] = "A";
            textNumbers[2] = "M";
        }
        else if (s >= 10)
        {
            textNumbers[0] = "";
            textNumbers[1] = (s % 10).ToString();
            textNumbers[2] = (s / 10 % 10).ToString();
        }
        else
        {
            textNumbers[0] = "";
            textNumbers[1] = "";
            textNumbers[2] = (s % 10).ToString();
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _textMeshProsLevelNumbers[j].text = textNumbers[j];
            }

            yield return new WaitForSeconds(0.08f);

            for (int j = 0; j < 3; j++)
            {
                _textMeshProsLevelNumbers[j].text = "";
            }

            yield return new WaitForSeconds(0.08f);
        }

        for (int i = 0; i < 3; i++)
        {
            _textMeshProsLevelNumbers[i].text = textNumbers[i];
        }
    }

    IEnumerator CoroutineDrawLifeWithFlicker(int s)
    {
        for (int i = 0; i < 8; i++)
        {
            _textMeshProsLifeGauges[s].text = "I";
            yield return new WaitForSeconds(0.08f);

            _textMeshProsLifeGauges[s].text = "";
            yield return new WaitForSeconds(0.08f);
        }

        _textMeshProsLifeGauges[s].text = "I";
    }

    IEnumerator CoroutineShowMessage(string message)
    {
        gameObjectTextMeshProMessage.transform.localPosition = new Vector3(0.0f, -100.0f, 0.0f);
        _textMeshProMessage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _textMeshProMessage.text = message;
        
        gameObjectTextMeshProMessage.transform.DOLocalMoveY(100.0f, 1.0f);
        _textMeshProMessage.DOColor(new Color(1.0f, 1.0f, 1.0f, 1.0f), 1.0f);
        yield return new WaitForSeconds(5.0f);
        gameObjectTextMeshProMessage.transform.DOLocalMoveY(300.0f, 0.4f);
        _textMeshProMessage.DOColor(new Color(1.0f, 1.0f, 1.0f, 0.0f), 1.0f);
    }
}
