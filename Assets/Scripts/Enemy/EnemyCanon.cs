using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanon : Enemy
{
    [SerializeField] private GameObject prefabDiminishEffect;

    private int _bulletAmount;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        StartCoroutine(CoroutineInitialization());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
                break;
        }
    }

    public void Initialize(string bodyColor, int bulletAmount)
    {
        _bodyColor = bodyColor;
        _bulletAmount = bulletAmount;
    }

    IEnumerator CoroutineInitialization()
    {
        _rigidbody2D.velocity = new Vector2(-100.0f, 0.0f);
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        _rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        for (int i = 0; i < _bulletAmount; i++)
        {
            float positionX = transform.position.x - 50.0f;
            float positionY = transform.position.y;
            _enemyManager.GenerateEnemyBullet(_bodyColor, positionX, positionY, -1500.0f, 0.0f);
            for (int j = 0; j < 10; j++)
            {
                yield return new WaitForFixedUpdate();
            }
        }

        for (int i = 0; i < 40; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        
        _rigidbody2D.velocity=new Vector2(100.0f,0.0f);

        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        
        Destroy(gameObject);
    }
}
