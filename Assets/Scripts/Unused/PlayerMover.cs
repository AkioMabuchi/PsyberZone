using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float movingSpeed;
    private float _mx;
    private float _my;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _mx = Input.GetAxis("Horizontal");
        _my = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        float vx = _mx * movingSpeed;
        float vy = _my * movingSpeed;
        _rigidbody2D.velocity = new Vector2(vx,vy);
    }
}
