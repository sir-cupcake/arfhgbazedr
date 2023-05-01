using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private bool _isRunning = false;


    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _jumpDuration;
    private float _jumpTimer = 0;
    private bool _isJumping = false;

    [SerializeField] private LayerMask _obstacleLayer;
    private float _height;

    private void Start()
    {
        _isRunning = true;
        _height = transform.position.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !_isJumping)
        {
            _isJumping = true;
        }


        if (_isRunning)
        {
            transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
        }

        if (_isJumping)
        {
            _jumpTimer += Time.deltaTime;
            transform.position = new Vector2(transform.position.x, _jumpCurve.Evaluate(_jumpTimer / _jumpDuration) + _height);
            if (_jumpTimer > _jumpDuration)
            {
                _isJumping = false;
                _jumpTimer = 0;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_obstacleLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            Die();
    }

    private void Die()
    {
        _isRunning = false;
        Debug.Log("бобик сдох");
    }
}