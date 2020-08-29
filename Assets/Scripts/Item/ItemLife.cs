using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLife : Item
{
    [SerializeField] private GameObject prefabRecoverySound;

    private float _speedX;
    private float _soeedY;
    
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
        _rigidbody2D.velocity = new Vector2(_speedX, _soeedY);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                if (_mainGameStateManager.GetLife() > 0)
                {
                    Instantiate(prefabRecoverySound);

                    _mainGameStateManager.RecoverLife();

                    Destroy(gameObject);
                }

                break;
        }
    }

    public void Initialize(float speedX, float speedY)
    {
        _speedX = speedX;
        _soeedY = speedY;
    }
}
