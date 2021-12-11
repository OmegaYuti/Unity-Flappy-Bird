using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _maxRotationAngle = 0.8f;
    [SerializeField] private float _velocityScale = 0.02f;
    [SerializeField] private float _jumpForce = 20000f;

    public event Action Death;
    public event Action TubePassed;

    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        RotateByVelocity();
    }
    public void Jump()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
    private void RotateByVelocity()
    {
        if (_rigidbody.velocity.y == 0)
            return;

        var impulse = _rigidbody.velocity.y * _velocityScale;
        var clampedImpulse = Mathf.Clamp(impulse, -1f, 1f);
        var angle = _maxRotationAngle * clampedImpulse;

        transform.eulerAngles = Vector3.forward * angle;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Death();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject;

        if (collisionObject.TryGetComponent<TubePair>(out _))
            TubePassed();
    }
}
