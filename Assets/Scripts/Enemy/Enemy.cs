using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected MainGameStateManager _mainGameStateManager;
    protected EnemyManager _enemyManager;

    protected SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rigidbody2D;

    protected string _bodyColor;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _mainGameStateManager = GameObject.Find("MainGameStateManager").GetComponent<MainGameStateManager>();
        _enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        switch (_bodyColor)
        {
            case "White":
                _spriteRenderer.color = new Color(1.0f,1.0f,1.0f);
                break;
            case "Yellow":
                _spriteRenderer.color = new Color(1.0f,1.0f,0.5f);
                break;
            case "Cyan":
                _spriteRenderer.color = new Color(0.5f,1.0f,1.0f);
                break;
            case "Magenta":
                _spriteRenderer.color = new Color(1.0f, 0.5f, 1.0f);
                break;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
