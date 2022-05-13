using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpVelocity;
    public float lerpTime;
    public float wallDetectionDistance;
    public float wallDetectionRadius;

    public LayerMask groundLayer;

    private bool _floor;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _floor = Physics2D.OverlapCircle(transform.position + Vector3.down * wallDetectionDistance, wallDetectionRadius,
            groundLayer);

        var horizontal = Input.GetAxis("Horizontal");
        var jump = Input.GetButtonDown("Jump");

        HorizontalMove(Mathf.Lerp(_rb.velocity.x, horizontal * speed, lerpTime));
        
        if (_floor && _rb.velocity.y < 1e-3 && jump)
        {
            VerticalMove(jumpVelocity);
        }
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

        Gizmos.DrawWireSphere(transform.position + Vector3.down * wallDetectionDistance, wallDetectionRadius);
    }
}