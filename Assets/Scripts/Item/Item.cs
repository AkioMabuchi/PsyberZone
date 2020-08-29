using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected MainGameStateManager _mainGameStateManager;

    protected SpriteRenderer _spriteRenderer;

    protected Rigidbody2D _rigidbody2D;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _mainGameStateManager = GameObject.Find("MainGameStateManager").GetComponent<MainGameStateManager>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
