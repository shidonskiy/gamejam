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

    private float _horizontal;
    private bool _jump;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _jump |= Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        _floor = Physics2D.OverlapCircle(transform.position + Vector3.down * wallDetectionDistance, wallDetectionRadius,
            groundLayer);
        
        HorizontalMove(Mathf.Lerp(_rb.velocity.x, _horizontal * speed, lerpTime));
        
        if (_floor && _rb.velocity.y < 1e-3 && _jump)
        {
            VerticalMove(jumpVelocity);
            _jump = false;
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