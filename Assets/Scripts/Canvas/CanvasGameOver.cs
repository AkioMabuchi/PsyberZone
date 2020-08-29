using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGameOver : MonoBehaviour
{
    private MainGameStateManager _mainGameStateManager;

    [SerializeField] private GameObject gameObjectTextMeshProScoreNumbers;

    private TextMeshProUGUI _textMeshProScoreNumbers;
    
    private bool _isLoaded;
    // Start is called before the first frame update
    void Start()
    {
        _mainGameStateManager = GameObject.Find("MainGameStateManager").GetComponent<MainGameStateManager>();

        _textMeshProScoreNumbers = gameObjectTextMeshProScoreNumbers.GetComponent<TextMeshProUGUI>();

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

    public void Terminated()
    {
        
    }

    IEnumerator CoroutineInitialize()
    {
        while (!_isLoaded)
        {
            yield return null;
        }

        _textMeshProScoreNumbers.text = _mainGameStateManager.GetScore().ToString();
    }
}
