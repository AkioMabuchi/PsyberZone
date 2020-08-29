using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private Sprite spritePlayer;
    [SerializeField] private Sprite spritePlayerFading1;
    [SerializeField] private Sprite spritePlayerFading2;
    
    [SerializeField] private int initialHP;
    
    private SpriteRenderer _spriteRenderer;

    private int _currentHP;

    private bool _isKnockBack;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _currentHP = initialHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Enemy":
                if (!_isKnockBack)
                {
                    _isKnockBack = true;
                    StartCoroutine(CoroutineKnockBack());
                }
                break;
        }
    }

    IEnumerator CoroutineKnockBack()
    {
        for (int i = 0; i < 10; i++)
        {
            _spriteRenderer.sprite = spritePlayerFading1;
            for (int j = 0; j < 5; j++)
            {
                yield return new WaitForFixedUpdate();
            }

            _spriteRenderer.sprite = spritePlayerFading2;
            for (int j = 0; j < 5; j++)
            {
                yield return new WaitForFixedUpdate();
            }

        }

        _spriteRenderer.sprite = spritePlayer;
        _isKnockBack = false;
    }
}
