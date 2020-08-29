using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ES3Types;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    private MainSceneManager _mainSceneManager;

    private MainGameStateManager _mainGameStateManager;

    private CanvasMain _canvasMain;
    
    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody2D;

    private AudioSource[] _audioSources;

    [SerializeField] private Sprite spritePlayer;
    [SerializeField] private Sprite spritePlayerFading1;
    [SerializeField] private Sprite spritePlayerFading2;
    [SerializeField] private Sprite spriteDummy;

    [SerializeField] private GameObject prefabEffectDiminish;
    [SerializeField] private GameObject prefabShootingSound;
    [SerializeField] private GameObject prefabNormalBullet;
    
    [SerializeField] private float movingSpeed;

    [SerializeField] private string initialBodyColor;
    
    private float _mx;
    private float _my;

    private string _bodyColor;

    private bool _isKnockBack;

    // Start is called before the first frame update
    void Start()
    {
        _mainSceneManager = GameObject.Find("MainSceneManager").GetComponent<MainSceneManager>();
        _mainGameStateManager = GameObject.Find("MainGameStateManager").GetComponent<MainGameStateManager>();
        _canvasMain = GameObject.Find("CanvasMain").GetComponent<CanvasMain>();
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSources = GetComponents<AudioSource>();
        
        _bodyColor = initialBodyColor;
        
        switch (_bodyColor)
        {
            case "Yellow":
                _spriteRenderer.color = new Color(1.0f,1.0f,0.5f);
                break;
            case "Cyan":
                _spriteRenderer.color = new Color(0.5f,1.0f,1.0f);
                break;
            case "Magenta":
                _spriteRenderer.color = new Color(1.0f,0.5f,1.0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainGameStateManager.GetLife() > 0) 
        {
            _mx = Input.GetAxis("Horizontal");
            _my = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.J))
            {
                Vector3 currentPosition = transform.position;
                float px = currentPosition.x + 50.0f;
                float py = currentPosition.y;
                float pz = currentPosition.z;
                Instantiate(prefabShootingSound);
                Instantiate(prefabNormalBullet, new Vector3(px, py, pz), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f))
                    .GetComponent<NormalBullet>().Initialize(_bodyColor);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                switch (_bodyColor)
                {
                    case "Yellow":
                        _bodyColor = "Cyan";
                        _spriteRenderer.color = new Color(0.5f, 1.0f, 1.0f);
                        break;
                    case "Cyan":
                        _bodyColor = "Magenta";
                        _spriteRenderer.color = new Color(1.0f, 0.5f, 1.0f);
                        break;
                    case "Magenta":
                        _bodyColor = "Yellow";
                        _spriteRenderer.color = new Color(1.0f, 1.0f, 0.5f);
                        break;
                }
            }
        }
        else
        {
            _mx = 0.0f;
            _my = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        float vx = _mx * movingSpeed;
        float vy = _my * movingSpeed;
        _rigidbody2D.velocity = new Vector2(vx, vy);
        float px = transform.position.x;
        float py = transform.position.y;
        if (px >= 800.0f)
        {
            _mainGameStateManager.SetPositionBonus(5);
        }
        else if (px >= 480.0f)
        {
            _mainGameStateManager.SetPositionBonus(3);
        }
        else if (px >= 0.0f)
        {
            _mainGameStateManager.SetPositionBonus(2);
        }
        else
        {
            _mainGameStateManager.SetPositionBonus(1);
        }

        _canvasMain.FadeLeftUIs(px < -400.0f && py > 330.0f && _mainGameStateManager.GetLife() > 0);
        _canvasMain.FadeRightUIs(px > 250.0f & py > 330.0f && _mainGameStateManager.GetLife() > 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_mainGameStateManager.GetLife() > 0)
        {
            switch (other.tag)
            {
                case "Enemy":
                    if (!_isKnockBack)
                    {
                        if (_mainSceneManager.IsTutorial())
                        {
                            _isKnockBack = true;
                            StartCoroutine(CoroutineKnockBack());
                        }
                        else
                        {
                            _mainGameStateManager.DecreaseLife();
                            if (_mainGameStateManager.GetLife() > 0)
                            {
                                _isKnockBack = true;
                                _audioSources[0].time = 0.05f;
                                _audioSources[0].Play();
                                StartCoroutine(CoroutineKnockBack());
                            }
                            else
                            {
                                Instantiate(prefabEffectDiminish, transform.position,
                                    new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                                _spriteRenderer.sprite = spriteDummy;
                                _audioSources[1].time = 0.15f;
                                _audioSources[1].Play();
                                StartCoroutine(CoroutineDefeated());
                            }
                        }
                    }

                    break;
            }
        }
    }

    IEnumerator CoroutineKnockBack()
    {
        for (int i = 0; i < 10; i++)
        {
            _spriteRenderer.sprite = spritePlayerFading1;
            for (int j = 0; j < 4; j++)
            {
                yield return new WaitForFixedUpdate();
            }

            _spriteRenderer.sprite = spritePlayerFading2;
            for (int j = 0; j < 4; j++)
            {
                yield return new WaitForFixedUpdate();
            }
        }

        _spriteRenderer.sprite = spritePlayer;
        _isKnockBack = false;
    }

    IEnumerator CoroutineDefeated()
    {
        
        yield return new WaitForSeconds(3.0f);
        _mainSceneManager.GameOver();
    }

    public void Retry()
    {
        transform.position = new Vector3(-800.0f, 0.0f, 0.0f);
        _spriteRenderer.sprite = spritePlayer;
        _bodyColor = initialBodyColor;
        switch (_bodyColor)
        {
            case "Yellow":
                _spriteRenderer.color = new Color(1.0f,1.0f,0.5f);
                break;
            case "Cyan":
                _spriteRenderer.color = new Color(0.5f,1.0f,1.0f);
                break;
            case "Magenta":
                _spriteRenderer.color = new Color(1.0f,0.5f,1.0f);
                break;
        }
    }
}
