using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : Enemy
{
    [SerializeField] private Sprite[] sprites = new Sprite[10];
    [SerializeField] private GameObject prefabIncreaseSound;
    [SerializeField] private GameObject prefabCountSound;
    [SerializeField] private GameObject prefabDiminishEffect;

    private int _bodyCount;
    
    private float _speedX;
    private float _speedY;

    private float _increaseSpeedX;
    private float _increaseSpeedY;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        _spriteRenderer.sprite = sprites[_bodyCount];
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
                    if (_bodyCount > 1)
                    {
                        Instantiate(prefabCountSound);
                        _bodyCount--;
                        _spriteRenderer.sprite = sprites[_bodyCount];
                        _mainGameStateManager.AddScore(100);
                    }
                    else
                    {
                        Vector3 currentPosition = transform.position;
                        Instantiate(prefabDiminishEffect, currentPosition, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
                        Destroy(gameObject);
                        _mainGameStateManager.AddScore(2000);
                    }
                }
                else if (_bodyCount < 9)
                {
                    Instantiate(prefabCountSound);
                    _bodyCount++;
                    _spriteRenderer.sprite = sprites[_bodyCount];
                    _mainGameStateManager.AddScore(10);
                }
                else
                {
                    Instantiate(prefabIncreaseSound);
                    for (int i = 1; i <= 3; i++)
                    {
                        float positionX = transform.position.x;
                        float positionY = transform.position.y;
                        float speedX = _speedX;
                        float speedY = Random.Range(-100.0f, 100.0f);
                        int bodyCount = 5;
                        _enemyManager.GenerateEnemyCount(_bodyColor, positionX, positionY, speedX, speedY, i,
                            bodyCount);
                    }
                    Destroy(gameObject);
                    _mainGameStateManager.AddScore(10);
                }
                break;
        }
    }

    public void Initialize(string bodyColor, float speedX, float speedY, int increaseCode, int bodyCount)
    {
        float[] increaseSpeedsX = {0.0f, 1000.0f, 1000.0f, 1000.0f};
        float[] increaseSpeedsY = {0.0f, 1000.0f, 0.0f, -1000.0f};
        _bodyColor = bodyColor;
        _speedX = speedX;
        _speedY = speedY;
        _increaseSpeedX = increaseSpeedsX[increaseCode];
        _increaseSpeedY = increaseSpeedsY[increaseCode];
        _bodyCount = bodyCount;
    } 
}
