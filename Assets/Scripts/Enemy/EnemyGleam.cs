using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGleam : Enemy
{
    [SerializeField] private GameObject prefabIncreaseSound;
    [SerializeField] private GameObject prefabDiminishEffect;
    
    private float _speedX;
    private float _speedY;

    private float _increaseSpeedX;
    private float _increaseSpeedY;

    private int _intervalCount;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        StartCoroutine(CoroutineIntroduction());
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
                    Instantiate(prefabDiminishEffect, transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                    Destroy(gameObject);
                    _mainGameStateManager.AddScore(10000);
                }
                else
                {
                    Instantiate(prefabIncreaseSound);
                    for (int i = 1; i <= 2; i++)
                    {
                        float positionX = transform.position.x;
                        float positionY = transform.position.y;
                        float speedX = _speedX;
                        float speedY = _speedY;
                        _enemyManager.GenerateEnemyGleam(_bodyColor, positionX, positionY, speedX, speedY, i,
                            _intervalCount);
                    }
                    Destroy(gameObject);
                    _mainGameStateManager.AddScore(10);
                }
                break;
        }
    }

    public void Initialize(string bodyColor, float speedX, float speedY, int increaseCode, int intervalCount)
    {
        float[] increaseSpeedsX = {0.0f, 1000.0f, 1000.0f};
        float[] increaseSpeedsY = {0.0f, -1500.0f, 1500.0f};
        _bodyColor = bodyColor;
        _speedX = speedX;
        _speedY = speedY;
        _increaseSpeedX = increaseSpeedsX[increaseCode];
        _increaseSpeedY = increaseSpeedsY[increaseCode];
        _intervalCount = intervalCount;
    }

    IEnumerator CoroutineIntroduction()
    {
        while (true)
        {
            for (int i = 0; i < _intervalCount; i++) 
            {
                yield return new WaitForFixedUpdate();
            }

            for (int i = 0; i < 8; i++)
            {
                float[] cos = {1.0f, 0.707f, 0.0f, -0.707f, -1.0f, -0.707f, 0.0f, 0.707f};
                float[] sin = {0.0f, 0.707f, 1.0f, 0.707f, 0.0f, -0.707f, -1.0f, -0.707f};
                float basePosition = 50.0f;
                float baseSpeed = 800.0f;
                float positionX = basePosition * cos[i] + transform.position.x;
                float positionY = basePosition * sin[i] + transform.position.y;
                float speedX = baseSpeed * cos[i] + _speedX;
                float speedY = baseSpeed * sin[i] + _speedY;
                _enemyManager.GenerateEnemyBullet(_bodyColor, positionX, positionY, speedX, speedY);
            }
        }
    }
}
