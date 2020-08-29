using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoint : Item
{
    [SerializeField] private GameObject prefabPointSound;

    private int _score;
    
    private float _speedX;
    private float _speedY;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_speedX, _speedY);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                if (_mainGameStateManager.GetLife() > 0)
                {
                    Instantiate(prefabPointSound);

                    _mainGameStateManager.AddScore(_score);
                    Destroy(gameObject);
                }

                break;
        }
    }

    public void Initialize(float speedX, float speedY, int score)
    {
        _speedX = speedX;
        _speedY = speedY;
        _score = score;
    }
}
