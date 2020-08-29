using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Enemy
{
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
            case "NormalBullet":
                NormalBullet normalBullet = other.gameObject.GetComponent<NormalBullet>();
                string bulletColor = normalBullet.GetBulletColor();

                Destroy(other.gameObject);

                if (_bodyColor == bulletColor)
                {
                    Destroy(gameObject);
                    _mainGameStateManager.AddScore(100);
                }

                break;
        }
    }

    public void Initialize(string bodyColor, float speedX, float speedY)
    {
        _bodyColor = bodyColor;
        _speedX = speedX;
        _speedY = speedY;
    } 
}
