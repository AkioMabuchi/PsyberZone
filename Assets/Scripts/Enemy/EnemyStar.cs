using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class EnemyStar : Enemy
{
    [SerializeField] private GameObject prefabIncreaseSound;

    [SerializeField] private GameObject prefabDiminishEffect;
    
    private float _speedX;
    private float _speedY;

    private float _increaseSpeedX;
    private float _increaseSpeedY;
    
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
        float vx = _speedX + _increaseSpeedX;
        float vy = _speedY + _increaseSpeedY;
        _rigidbody2D.velocity = new Vector2(vx, vy);
        _increaseSpeedX *= 0.9f;
        _increaseSpeedY *= 0.9f;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "NormalBullet":
                NormalBullet normalBullet = other.gameObject.GetComponent<NormalBullet>();
                string bulletColor = normalBullet.GetBulletColor();

                Destroy(other.gameObject);

                if (_bodyColor == bulletColor)
                {
                    Vector3 currentPosition = transform.position;
                    Instantiate(prefabDiminishEffect, currentPosition, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    Destroy(gameObject);
                    _mainGameStateManager.AddScore(8000);
                }
                else
                {
                    Vector3 currentPosition = transform.position;
                    Instantiate(prefabIncreaseSound);
                    for (int i = 1; i <= 3; i++)
                    {
                        float positionX = transform.position.x;
                        float positionY = transform.position.y;
                        float speedX = _speedX;
                        float speedY = _speedY;
                        _enemyManager.GenerateEnemyStar(_bodyColor, positionX, positionY, speedX, speedY, i);
                    }
                    Destroy(gameObject);
                    _mainGameStateManager.AddScore(10);
                }
                break;
        }
    }
    
    public void Initialize(string bodyColor, float speedX, float speedY, int increaseCode)
    {
        float[] increaseSpeedsX = {0.0f, 5000.0f, 5000.0f, 5000.0f};
        float[] increaseSpeedsY = {0.0f, 1000.0f, 0.0f, -1000.0f};
        _bodyColor = bodyColor;
        _speedX = speedX;
        _speedY = speedY;
        _increaseSpeedX = increaseSpeedsX[increaseCode];
        _increaseSpeedY = increaseSpeedsY[increaseCode];
    } 
}
