using System;
using System.Collections;
using System.Numerics;
using GameJam.Scripts.Obstacles.States;
using GameJam.Scripts.Utils;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpVelocity;
    public float lerpTime;
    public float floorDetectionDistance;
    public float floorDetectionRadius;
    public float sideWallDetectionDistance;
    public float sideWallDetectionHeight;
    public float sideWallDetectionWidth;

    public LayerMask groundLayer;
    
    private bool _leftWall;
    private bool _rightWall;
    private bool _floor;

    private Rigidbody2D _rb;

    private float _horizontal;
    private float _horizontalPrevious;
    private bool _jump;

    private bool _movementBlocked;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movementBlocked = false;
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _jump |= Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        _floor = Physics2D.OverlapCircle(transform.position + Vector3.down * floorDetectionDistance,
            floorDetectionRadius, groundLayer);
        _leftWall = Physics2D.OverlapArea(
            transform.position + Vector3.left * (sideWallDetectionDistance - sideWallDetectionWidth) +
            Vector3.up * sideWallDetectionHeight / 2,
            transform.position + Vector3.left * (sideWallDetectionDistance + sideWallDetectionWidth) +
            Vector3.down * sideWallDetectionHeight / 2, groundLayer);

        _rightWall = Physics2D.OverlapArea(
            transform.position + Vector3.right * (sideWallDetectionDistance - sideWallDetectionWidth) +
            Vector3.up * sideWallDetectionHeight / 2,
            transform.position + Vector3.right * (sideWallDetectionDistance + sideWallDetectionWidth) +
            Vector3.down * sideWallDetectionHeight / 2, groundLayer);

        if (!_movementBlocked)
        {
            if (!_floor)
            {
                if (_leftWall)
                {
                    _horizontal = Mathf.Max(_horizontal, 0f);
                }
                else if (_rightWall)
                {
                    _horizontal = Mathf.Min(_horizontal, 0f);
                }
            }

            //Stopping player only if it was previously moving (to enable moving with platforms)
            if (Mathf.Abs(_horizontalPrevious) > 1e-3 || Mathf.Abs(_horizontal) > 1e-3)
            {
                HorizontalMove(Mathf.Lerp(_rb.velocity.x, _horizontal * speed, lerpTime));
            }

            if (_floor && _jump)
            {
                VerticalMove(jumpVelocity);
            }
        }

        _horizontalPrevious = _horizontal;
        _jump = false;
    }

    private void HorizontalMove(float velocityX)
    {
        Vector2 velocity = _rb.velocity;
        velocity.x = velocityX;
        _rb.velocity = velocity;
    }

    private void VerticalMove(float velocityY)
    {
        Vector2 velocity = _rb.velocity;
        velocity.y = velocityY;
        _rb.velocity = velocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + Vector3.down * floorDetectionDistance, floorDetectionRadius);
        Gizmos.DrawWireCube(transform.position + Vector3.left * sideWallDetectionDistance,
            new Vector3(sideWallDetectionWidth, sideWallDetectionHeight, 0f));
        Gizmos.DrawWireCube(transform.position + Vector3.right * sideWallDetectionDistance,
            new Vector3(sideWallDetectionWidth, sideWallDetectionHeight, 0f));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == Layers.Bouncer)
        {
            if (col.TryGetComponent(out BouncingObjectState bouncer))
            {
                StartCoroutine(blockMovement(bouncer._movementBlockTime));
                _rb.velocity =bouncer._bounceDirection.localPosition * bouncer._bounceForce;
            }
        }
    }
    
    IEnumerator blockMovement(float blockTime) {
        _movementBlocked = true;
        yield return new WaitForSeconds(blockTime);
        _movementBlocked = false;
    }
}