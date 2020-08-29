using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    private string _bulletColor;

    // Start is called before the first frame update
    protected override void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        switch (_bulletColor)
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
                _spriteRenderer.color = new Color(1.0f,0.5f,1.0f);
                break;
        }

        _rigidbody2D.velocity = new Vector2(2000.0f, 0.0f);
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    public void Initialize(string bulletColor)
    {
        _bulletColor = bulletColor;
    }

    public string GetBulletColor()
    {
        return _bulletColor;
    }
}
