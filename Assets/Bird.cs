using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UI _ui;

    [Header("Parameters")]
    [SerializeField] private float _maxRotationAngle = 0.8f;
    [SerializeField] private float _velocityScale = 0.02f;
    [SerializeField] private float _jumpForce = 20000f;
    [SerializeField] private Vector3 _startPosition;

    public event Action Death;
    public event Action TubePassed;
    public event Action StartMoving;

    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;

    private bool _canStartGame = true;
    private bool _gameInProgress = false;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();

        _ui.EndDeathMessage += Respawn;
    }
    private void Update()
    {
        if (CanJump() && _gameInProgress)
            Jump();
        else if (CanJump() && _canStartGame)
            StartGame();
        else if (!_gameInProgress)
            return;
        RotateByVelocity();
    }
    private void OnDisable()
    {
        _ui.EndDeathMessage -= Respawn;
    }

    private bool CanJump()
    {
        return Input.GetMouseButtonDown(0);
    }
    private void Jump()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
    private void StartGame()
    {
        StartMoving();
        _rigidbody.simulated = true;
        _gameInProgress = true;
        _canStartGame = false;
    }
    private void Respawn()
    {
        _rigidbody.simulated = false;
        _collider.enabled = true;
        transform.localPosition = _startPosition;
        _rigidbody.velocity = Vector2.zero;
        RotateByVelocity();
        _canStartGame = true;
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
        _collider.enabled = false;
        _gameInProgress = false;
        Death();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject;

        if (collisionObject.TryGetComponent<TubePair>(out _))
            TubePassed();
    }
}
